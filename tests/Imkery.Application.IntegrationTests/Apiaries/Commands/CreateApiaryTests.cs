using FluentAssertions;
using Imkery.Application.IntegrationTests.Common;
using Imkery.TestShared.Apiaries;
using MediatR;

namespace Imkery.Application.IntegrationTests.Apiaries.Commands;

[Collection(MediatorFactoryCollection.CollectionName)]
public class CreateApiaryTests(MediatorFactory _mediatorFactory)
{
    private readonly IMediator _mediator = _mediatorFactory.CreateMediator();

    [Fact]
    public async void CreateApiary_ShouldCreateApiary_WhenCommandIsValid()
    {
        // Arrange
        var createApiaryCommand = ApiaryCommandsFactory.CreateApiaryCommand();
        
        // Act
        var createApiaryResult = await _mediator.Send(createApiaryCommand);
        
        // Assert
        createApiaryResult.IsError.Should().BeFalse();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(255)]
    public async Task CreateApiary_ShouldReturnValidationError_WhenCommandContainsInvalidData(int nameLength)
    {
        // Arrange
        string apiaryName = new('a', nameLength);
        var createApiaryCommand = ApiaryCommandsFactory.CreateApiaryCommand(name: apiaryName);
        
        // Act
        var createApiaryResult = await _mediator.Send(createApiaryCommand);

        // Assert
        createApiaryResult.IsError.Should().BeTrue();
    }
}