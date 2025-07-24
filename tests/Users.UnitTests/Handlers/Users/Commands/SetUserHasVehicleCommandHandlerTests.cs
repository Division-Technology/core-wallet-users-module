// <copyright file="SetUserHasVehicleCommandHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Xunit;
using Users.Application.Exceptions;

namespace Users.UnitTests.Handlers.Users.Commands;

public class SetUserHasVehicleCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SetUserHasVehicleCommandHandler _handler;

    public SetUserHasVehicleCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new SetUserHasVehicleCommandHandler(_mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldDelegateToPatchUpdateCommand()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var hasVehicle = true;
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = hasVehicle
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User vehicle status updated successfully"
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
            cmd.HasVehicle == hasVehicle &&
            cmd.FirstName == null &&
            cmd.LastName == null &&
            cmd.Language == null &&
            cmd.PhoneNumber == null &&
            cmd.IsBlocked == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullUserId_ShouldThrowBadRequestException()
    {
        // Arrange
        var hasVehicle = false;
        var request = new SetUserHasVehicleCommand
        {
            UserId = null,
            HasVehicle = hasVehicle
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_WithDifferentHasVehicleValues_ShouldPassCorrectValue(bool hasVehicle)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = hasVehicle
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User vehicle status updated"
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
            cmd.HasVehicle == hasVehicle), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = true
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
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = false
        };

        var cancellationToken = new CancellationToken(true); // Cancelled token
        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User vehicle status updated"
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
    public async Task Handle_SettingHasVehicleToTrue_ShouldSetHasVehicleToTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = true
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User has vehicle set to true"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.HasVehicle == true), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_SettingHasVehicleToFalse_ShouldSetHasVehicleToFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = false
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "User has vehicle set to false"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.HasVehicle == false), It.IsAny<CancellationToken>()), Times.Once);
    }
} 