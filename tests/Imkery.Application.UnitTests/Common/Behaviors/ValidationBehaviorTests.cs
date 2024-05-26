using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Imkery.Application.Apiaries.Commands.CreateApiary;
using Imkery.Application.Common.Behaviors;
using Imkery.Domain.Apiaries;
using Imkery.TestShared;
using Imkery.TestShared.Apiaries.Commands;
using MediatR;
using NSubstitute;

namespace Imkery.Application.UnitTests;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task InvokeBehavior_ShouldInvokeNextBehavior_WhenValidatorResultIsValid()
    {
        // Arrange

        // Create a request
        var command = ApiaryCommandsFactory.CreateApiaryCommand();
        
        // Create a next behavior
        var apiary = new ApiaryBuilder().Build();
        var nextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Apiary>>>();
        nextBehavior.Invoke().Returns(apiary);
        
        // Create validation behavior 
        var validator = Substitute.For<IValidator<CreateApiaryCommand>>();
        validator
            .ValidateAsync(command, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        var validationBehavior = new ValidationBehavior<CreateApiaryCommand, ErrorOr<Apiary>>(validator);

        // validator.ValidateAsync(command, Args.Any(CancellationToken));

        // Act 
        var result = await validationBehavior.Handle(command, nextBehavior, Arg.Any<CancellationToken>());

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(apiary);
    }
}