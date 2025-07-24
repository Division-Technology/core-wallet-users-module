// <copyright file="SetUserBlockedCommandHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Domain.Entities.Users.Commands.SetBlocked;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Xunit;
using Users.Application.Exceptions;

namespace Users.UnitTests.Handlers.Users.Commands;

public class SetUserBlockedCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SetUserBlockedCommandHandler _handler;

    public SetUserBlockedCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new SetUserBlockedCommandHandler(_mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldDelegateToPatchUpdateCommand()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var isBlocked = true;
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = isBlocked
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User blocked status updated successfully"
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
            cmd.IsBlocked == isBlocked &&
            cmd.FirstName == null &&
            cmd.LastName == null &&
            cmd.Language == null &&
            cmd.PhoneNumber == null &&
            cmd.HasVehicle == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullUserId_ShouldThrowBadRequestException()
    {
        // Arrange
        var isBlocked = false;
        var request = new SetUserBlockedCommand
        {
            UserId = null,
            IsBlocked = isBlocked
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_WithDifferentBlockedValues_ShouldPassCorrectValue(bool isBlocked)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = isBlocked
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User blocked status updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.Id == userId &&
            cmd.IsBlocked == isBlocked), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = true
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
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = false
        };

        var cancellationToken = new CancellationToken(true); // Cancelled token
        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User blocked status updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_BlockingUser_ShouldSetIsBlockedToTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = true
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User blocked successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.IsBlocked == true), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UnblockingUser_ShouldSetIsBlockedToFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = false
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User unblocked successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.IsBlocked == false), It.IsAny<CancellationToken>()), Times.Once);
    }
} 