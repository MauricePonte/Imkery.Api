using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Hives;
using Imkery.Infrastructure.Apiaries.Persistence;
using Imkery.Infrastructure.Common.Interceptors;
using Imkery.Infrastructure.Common.Middlewares;
using Imkery.Infrastructure.Common.Persistence;
using Imkery.Infrastructure.Hives.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Imkery.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddApplicationDbContext()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlite("Data source = Imkery.db");
            options.AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IApiariesRepository, ApiariesRepository>()
            .AddScoped<IHivesRepository, HivesRepository>();
        return services;
    }

    public static void EnsureDevelopmentInfrastructure(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext?.Database.EnsureDeleted();
            dbContext?.Database.EnsureCreated();
        }
    }

    public static void AddInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistancyMiddleware>();
    }
}
