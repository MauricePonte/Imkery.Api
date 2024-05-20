using ErrorOr;

namespace Imkery.Api.Endpoints.Common.ErrorHandling;

public static class Problem
{
    public static IResult HandleProblem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Results.Problem();
        }

        if (errors.All(e => e.Type is ErrorType.Validation))
        {
            return HandleValidationProblems(errors);
        }

        return HandleProblem(errors[0]);
    }

    private static IResult HandleProblem(Error error)
    {
        return error.Type switch
        {
            ErrorType.Conflict => Results.Conflict(error),
            ErrorType.NotFound => Results.NotFound(error),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Forbidden => Results.Forbid(),
            _ => Results.Problem()
        };
    }

    private static IResult HandleValidationProblems(List<Error> errors)
    {
        var validationErrors = new Dictionary<string, string[]>();

        errors.ForEach(error =>
        {
            validationErrors.Add(error.Code, [error.Description]);
        });

        return Results.ValidationProblem(validationErrors);
    }
}
