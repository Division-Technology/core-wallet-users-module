// <copyright file="SetUserLanguageCommandHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Domain.Entities.Users.Commands.SetLanguage;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Xunit;
using Users.Application.Exceptions;

namespace Users.UnitTests.Handlers.Users.Commands;

public class SetUserLanguageCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SetUserLanguageCommandHandler _handler;

    public SetUserLanguageCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new SetUserLanguageCommandHandler(_mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldDelegateToPatchUpdateCommand()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var language = "en";
        var request = new SetUserLanguageCommand
        {
            UserId = userId,
            Language = language
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "Language updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);
        Assert.Equal(expectedResponse.Message, result.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.Id == userId &&
            cmd.Language == language &&
            cmd.FirstName == null &&
            cmd.LastName == null &&
            cmd.PhoneNumber == null &&
            cmd.IsBlocked == null &&
            cmd.HasVehicle == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullUserId_ShouldThrowBadRequestException()
    {
        // Arrange
        var language = "es";
        var request = new SetUserLanguageCommand
        {
            UserId = null,
            Language = language
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyLanguage_ShouldThrowBadRequestException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserLanguageCommand
        {
            UserId = userId,
            Language = string.Empty
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserLanguageCommand
        {
            UserId = userId,
            Language = "fr"
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.Handle(request, CancellationToken.None));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task Handle_WithCancellationToken_ShouldPassCancellationToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserLanguageCommand
        {
            UserId = userId,
            Language = "de"
        };

        var cancellationToken = new CancellationToken(true); // Cancelled token
        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "Language updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), cancellationToken), Times.Once);
    }
} 