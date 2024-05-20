using ErrorOr;
using FluentValidation;
using Imkery.Application.Apiaries.Commands.CreateApiary;
using Imkery.Application.Common.Behaviors;
using Imkery.Domain.Apiaries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Imkery.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}
