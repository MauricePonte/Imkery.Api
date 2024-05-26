using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Imkery.Application.Common.Behaviors;
using Imkery.Application.Apiaries.Commands.CreateApiary;
using Imkery.Domain.Apiaries;
using MediatR;
using NSubstitute;
using Imkery.TestShared.Apiaries;

namespace Imkery.Application.UnitTests;

public class ValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<ErrorOr<Apiary>> _nextBehavior;
    private readonly IValidator<CreateApiaryCommand> _validator;
    private readonly ValidationBehavior<CreateApiaryCommand, ErrorOr<Apiary>> _sut;

    public ValidationBehaviorTests()
    {
        _nextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Apiary>>>();
        _validator = Substitute.For<IValidator<CreateApiaryCommand>>();

        _sut = new ValidationBehavior<CreateApiaryCommand, ErrorOr<Apiary>>(_validator);
    }

    [Fact]
    public async Task InvokeBehavior_ShouldInvokeNextBehavior_WhenValidatorResultIsValid()
    {
        // Arrange
        var command = ApiaryCommandsFactory.CreateApiaryCommand();
        var apiary = new ApiaryBuilder().Build();        

        _validator
            .ValidateAsync(command, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _nextBehavior.Invoke().Returns(apiary);

        // Act 
        var result = await _sut.Handle(command, _nextBehavior, Arg.Any<CancellationToken>());

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(apiary);
    }
}