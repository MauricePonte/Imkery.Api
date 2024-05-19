using ErrorOr;
using Imkery.Domain.Apiaries;
using MediatR;

namespace Imkery.Application.Apiaries.Commands.CreateApiary;

public class CreateApiaryCommandBehavior
    : IPipelineBehavior<CreateApiaryCommand, ErrorOr<Apiary>>
{
    public async Task<ErrorOr<Apiary>> Handle(
        CreateApiaryCommand request,
        RequestHandlerDelegate<ErrorOr<Apiary>> next,
        CancellationToken cancellationToken)
    {
        var validator = new CreateApiaryCommandValidator();

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors
                .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                .ToList();
        }

        return await next();
    }
}