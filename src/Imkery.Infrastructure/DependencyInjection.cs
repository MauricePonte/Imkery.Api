using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Hives;
using Imkery.Infrastructure.Apiaries.Persistence;
using Imkery.Infrastructure.Common.Interceptors;
using Imkery.Infrastructure.Common.Persistence;
using Imkery.Infrastructure.Hives.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Imkery.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddApplicationDbContext()
            .AddIdentityDbContext()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddIdentityDbContext(this IServiceCollection services)
    {
        services.AddDbContext<UserDbContext>((serviceProvider, options) =>
        {
            options.UseSqlite("Data source = Identity.db");
        });

        services
            .AddIdentityCore<IdentityUser>()
            .AddEntityFrameworkStores<UserDbContext>();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlite("Data source = Imkery.db");
            options.AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<ApplicationDbContext>();
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IApiariesRepository, ApiariesRepository>()
            .AddScoped<IHivesRepository, HivesRepository>();
        return services;
    }
}
