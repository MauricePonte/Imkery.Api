using FluentValidation;

namespace Imkery.Application.Apiaries.Commands.CreateApiary;

public class CreateApiaryCommandValidator
    : AbstractValidator<CreateApiaryCommand>
{
    public CreateApiaryCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(255);
    }
}