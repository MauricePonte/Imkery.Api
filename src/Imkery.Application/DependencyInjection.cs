using FluentValidation;
using Imkery.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Imkery.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(ExceptionHandlingBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
