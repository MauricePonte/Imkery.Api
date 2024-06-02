using MediatR;
using Microsoft.Extensions.Logging;

namespace Imkery.Application.Common.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger
)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception for {RequestName}", typeof(TRequest).Name);
            throw;
        }
    }
}
