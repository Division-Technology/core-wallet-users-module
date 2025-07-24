// <copyright file="SetUserPhoneNumberCommandHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Xunit;
using Users.Application.Exceptions;

namespace Users.UnitTests.Handlers.Users.Commands;

public class SetUserPhoneNumberCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SetUserPhoneNumberCommandHandler _handler;

    public SetUserPhoneNumberCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new SetUserPhoneNumberCommandHandler(_mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldDelegateToPatchUpdateCommand()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var newPhoneNumber = "+1234567890";
        var request = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = newPhoneNumber
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "Phone number updated successfully"
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
            cmd.PhoneNumber == newPhoneNumber &&
            cmd.FirstName == null &&
            cmd.LastName == null &&
            cmd.Language == null &&
            cmd.IsBlocked == null &&
            cmd.HasVehicle == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullUserId_ShouldThrowBadRequestException()
    {
        // Arrange
        var newPhoneNumber = "+9876543210";
        var request = new SetUserPhoneNumberCommand
        {
            UserId = null,
            NewPhoneNumber = newPhoneNumber
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyPhoneNumber_ShouldThrowBadRequestException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = string.Empty
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = "+5551234567"
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
        var request = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = "+1112223333"
        };

        var cancellationToken = new CancellationToken(true); // Cancelled token
        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "Phone number updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), cancellationToken), Times.Once);
    }

    [Theory]
    [InlineData("+1234567890")]
    [InlineData("+9876543210")]
    [InlineData("+5551234567")]
    public async Task Handle_WithDifferentPhoneNumbers_ShouldPassCorrectPhoneNumber(string phoneNumber)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = phoneNumber
        };

        var expectedResponse = new PatchUpdateUserCommandResponse
        {
            Success = true,
            Message = "Phone number updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(cmd =>
            cmd.PhoneNumber == phoneNumber), It.IsAny<CancellationToken>()), Times.Once);
    }
} 