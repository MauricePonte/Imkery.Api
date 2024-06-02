using Imkery.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Imkery.Infrastructure.Common.Interceptors;

internal class PublishDomainEventsInterceptor(IPublisher _publisher, IHttpContextAccessor _httpContext)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        HandleTransactionalDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await HandleTransactionalDomainEvents(eventData.Context);
        HandleEventualConsistantDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task HandleTransactionalDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var domainEvents = dbContext.ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                return entity.PopTransactionalDomainEvents();
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    private void HandleEventualConsistantDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var domainEvents = dbContext.ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => 
            {
                return entity.PopEventualConsistantDomainEvents();
            })
            .ToList();

        var domainEventsQueue = _httpContext.HttpContext!.Items.TryGetValue("DomainEventsQueue", out var value) &&
            value is Queue<IDomainEvent> exsistingDomainEvents
            ? exsistingDomainEvents
            : [];
        
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        _httpContext.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
}
