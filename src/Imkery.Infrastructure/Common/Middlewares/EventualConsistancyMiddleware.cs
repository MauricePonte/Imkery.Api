using Imkery.Domain.Common;
using Imkery.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Imkery.Infrastructure.Common.Middlewares;

internal class EventualConsistancyMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context, IPublisher publisher, ApplicationDbContext applicationDbContext)
    {
        context.Response.OnCompleted(async () =>
        {
            var transaction = await applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                        value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch
            {
                throw; // This is picked up by the ExceptionBehavior?
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}