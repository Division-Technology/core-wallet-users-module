// <copyright file="UsersControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.Controllers;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Commands.SetLanguage;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;
using Users.Domain.Entities.Users.Commands.SetBlocked;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Domain.Entities.Users.Queries.GetUser;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Domain.Entities.Users.Queries.GetByPhone;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;
using Xunit;

namespace Users.UnitTests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetByPhoneAsync_WithValidPhoneNumber_ShouldReturnOkResult()
    {
        // Arrange
        var phoneNumber = "+1234567890";
        var expectedResponse = new GetUserByPhoneQueryResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = phoneNumber
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByPhoneQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetByPhoneAsync(phoneNumber);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetUserByPhoneQueryResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Id, response.Id);
        Assert.Equal(expectedResponse.PhoneNumber, response.PhoneNumber);

        _mediatorMock.Verify(m => m.Send(It.Is<GetUserByPhoneQuery>(q => q.PhoneNumber == phoneNumber), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByPhoneAsync_WithNullResponse_ShouldReturnNotFound()
    {
        // Arrange
        var phoneNumber = "+1234567890";

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByPhoneQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserByPhoneQueryResponse?)null);

        // Act
        var result = await _controller.GetByPhoneAsync(phoneNumber);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByTelegramIdAsync_WithValidTelegramId_ShouldReturnOkResult()
    {
        // Arrange
        var telegramId = 123456789L;
        var expectedResponse = new GetUserQueryResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            TelegramId = telegramId
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetByTelegramIdAsync(telegramId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetUserQueryResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Id, response.Id);
        Assert.Equal(expectedResponse.TelegramId, response.TelegramId);

        _mediatorMock.Verify(m => m.Send(It.Is<GetUserQuery>(q => q.TelegramId == telegramId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByTelegramIdAsync_WithNullResponse_ShouldReturnNotFound()
    {
        // Arrange
        var telegramId = 123456789L;

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserQueryResponse?)null);

        // Act
        var result = await _controller.GetByTelegramIdAsync(telegramId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByChatIdAsync_WithValidChatId_ShouldReturnOkResult()
    {
        // Arrange
        var chatId = 987654321L;
        var expectedResponse = new GetUserQueryResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            ChatId = chatId
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetByChatIdAsync(chatId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetUserQueryResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Id, response.Id);
        Assert.Equal(expectedResponse.ChatId, response.ChatId);

        _mediatorMock.Verify(m => m.Send(It.Is<GetUserQuery>(q => q.ChatId == chatId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByChatIdAsync_WithNullResponse_ShouldReturnNotFound()
    {
        // Arrange
        var chatId = 987654321L;

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserQueryResponse?)null);

        // Act
        var result = await _controller.GetByChatIdAsync(chatId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CheckHasVehicleAsync_WithUserId_ShouldReturnOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = true,
            UserExists = true,
            UserId = userId,
            FoundBy = "UserId"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CheckHasVehicleAsync(userId: userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CheckUserHasVehicleQueryResponse>(okResult.Value);
        Assert.Equal(expectedResponse.HasVehicle, response.HasVehicle);
        Assert.Equal(expectedResponse.UserExists, response.UserExists);
        Assert.Equal(expectedResponse.UserId, response.UserId);
        Assert.Equal(expectedResponse.FoundBy, response.FoundBy);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(q => q.UserId == userId && q.TelegramId == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicleAsync_WithTelegramId_ShouldReturnOkResult()
    {
        // Arrange
        var telegramId = 123456789L;
        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = false,
            UserExists = true,
            UserId = Guid.NewGuid(),
            FoundBy = "TelegramId"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CheckHasVehicleAsync(telegramId: telegramId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CheckUserHasVehicleQueryResponse>(okResult.Value);
        Assert.Equal(expectedResponse.HasVehicle, response.HasVehicle);
        Assert.Equal(expectedResponse.FoundBy, response.FoundBy);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(q => q.UserId == null && q.TelegramId == telegramId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetLanguageAsync_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var language = "en";
        var expectedResponse = new SetUserLanguageCommandResponse
        {
            Success = true,
            Message = "Language updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserLanguageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.SetLanguageAsync(userId, language);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<SetUserLanguageCommandResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Success, response.Success);
        Assert.Equal(expectedResponse.Message, response.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserLanguageCommand>(c => c.UserId == userId && c.Language == language), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetPhoneNumberAsync_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var phoneNumber = "+1234567890";
        var expectedResponse = new SetUserPhoneNumberCommandResponse
        {
            Success = true,
            Message = "Phone number updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserPhoneNumberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.SetPhoneNumberAsync(userId, phoneNumber);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<SetUserPhoneNumberCommandResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Success, response.Success);
        Assert.Equal(expectedResponse.Message, response.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserPhoneNumberCommand>(c => c.UserId == userId && c.NewPhoneNumber == phoneNumber), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetBlockedAsync_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var isBlocked = true;
        var expectedResponse = new SetUserBlockedCommandResponse
        {
            Success = true,
            Message = "User blocked successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserBlockedCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.SetBlockedAsync(userId, isBlocked);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<SetUserBlockedCommandResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Success, response.Success);
        Assert.Equal(expectedResponse.Message, response.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserBlockedCommand>(c => c.UserId == userId && c.IsBlocked == isBlocked), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetHasVehicleAsync_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var hasVehicle = true;
        var expectedResponse = new SetUserHasVehicleCommandResponse
        {
            Success = true,
            Message = "Vehicle status updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserHasVehicleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.SetHasVehicleAsync(userId, hasVehicle);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<SetUserHasVehicleCommandResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Success, response.Success);
        Assert.Equal(expectedResponse.Message, response.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserHasVehicleCommand>(c => c.UserId == userId && c.HasVehicle == hasVehicle), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProfileAsync_WithValidRequest_ShouldReturnNoContent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new PatchUpdateUserCommand
        {
            FirstName = "John",
            LastName = "Doe"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchUpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PatchUpdateUserCommandResponse { Success = true, Message = "Profile updated" });

        // Act
        var result = await _controller.UpdateProfileAsync(userId, command);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mediatorMock.Verify(m => m.Send(It.Is<PatchUpdateUserCommand>(c => c.Id == userId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicleAsync_WithBothIds_ShouldPrioritizeUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var telegramId = 123456789L;
        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = true,
            UserExists = true,
            UserId = userId,
            FoundBy = "UserId"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CheckHasVehicleAsync(userId, telegramId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CheckUserHasVehicleQueryResponse>(okResult.Value);
        Assert.Equal("UserId", response.FoundBy);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(q => q.UserId == userId && q.TelegramId == telegramId), It.IsAny<CancellationToken>()), Times.Once);
    }
} 