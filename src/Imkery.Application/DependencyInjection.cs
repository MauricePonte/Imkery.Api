using ErrorOr;
using Imkery.Application.Apiaries.Commands.CreateApiary;
using Imkery.Domain.Apiaries;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Imkery.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddBehavior<IPipelineBehavior<CreateApiaryCommand, ErrorOr<Apiary>>, CreateApiaryCommandBehavior>();
        });

        return services;
    }
}
