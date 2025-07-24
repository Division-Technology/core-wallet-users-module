// <copyright file="UsersGrpcServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc;
using Grpc.Core;
using MediatR;
using Moq;
using Users.Api.Grpc;
using Users.Grpc;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Commands.SetLanguage;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;
using Users.Domain.Entities.Users.Commands.SetBlocked;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;
using Users.Domain.Entities.Users.Queries.GetById;
using Users.Domain.Entities.Users.Queries.GetByPhone;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Domain.Enums;
using Users.Repositories.Users;
using Xunit;

namespace Users.UnitTests.Grpc;

public class UsersGrpcServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly UsersGrpcService _service;
    private readonly ServerCallContext _context;

    public UsersGrpcServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _service = new UsersGrpcService(_mediatorMock.Object, _usersRepositoryMock.Object);
        _context = new Mock<ServerCallContext>().Object;
    }

    [Fact]
    public async Task SetLanguage_WithValidRequest_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserLanguageRequest
        {
            Id = Guid.NewGuid().ToString(),
            Language = "en"
        };

        var expectedResponse = new SetUserLanguageCommandResponse
        {
            Success = true,
            Message = "Language updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserLanguageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetLanguage(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);
        Assert.Equal(expectedResponse.Message, result.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserLanguageCommand>(cmd =>
            cmd.UserId == Guid.Parse(request.Id) &&
            cmd.Language == request.Language), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetLanguage_WithEmptyId_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserLanguageRequest
        {
            Id = string.Empty,
            Language = "es"
        };

        var expectedResponse = new SetUserLanguageCommandResponse
        {
            Success = false,
            Message = "User not found"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserLanguageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetLanguage(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserLanguageCommand>(cmd =>
            cmd.UserId == null &&
            cmd.Language == request.Language), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetPhoneNumber_WithValidRequest_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserPhoneNumberRequest
        {
            Id = Guid.NewGuid().ToString(),
            NewPhoneNumber = "+1234567890"
        };

        var expectedResponse = new SetUserPhoneNumberCommandResponse
        {
            Success = true,
            Message = "Phone number updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserPhoneNumberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetPhoneNumber(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);
        Assert.Equal(expectedResponse.Message, result.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserPhoneNumberCommand>(cmd =>
            cmd.UserId == Guid.Parse(request.Id) &&
            cmd.NewPhoneNumber == request.NewPhoneNumber), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetPhoneNumber_WithEmptyId_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserPhoneNumberRequest
        {
            Id = string.Empty,
            NewPhoneNumber = "+9876543210"
        };

        var expectedResponse = new SetUserPhoneNumberCommandResponse
        {
            Success = false,
            Message = "User not found"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserPhoneNumberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetPhoneNumber(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserPhoneNumberCommand>(cmd =>
            cmd.UserId == null &&
            cmd.NewPhoneNumber == request.NewPhoneNumber), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetBlocked_WithValidRequest_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserBlockedRequest
        {
            Id = Guid.NewGuid().ToString(),
            IsBlocked = true
        };

        var expectedResponse = new SetUserBlockedCommandResponse
        {
            Success = true,
            Message = "User blocked successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserBlockedCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetBlocked(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);
        Assert.Equal(expectedResponse.Message, result.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserBlockedCommand>(cmd =>
            cmd.UserId == Guid.Parse(request.Id) &&
            cmd.IsBlocked == request.IsBlocked), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetBlocked_WithEmptyId_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserBlockedRequest
        {
            Id = string.Empty,
            IsBlocked = false
        };

        var expectedResponse = new SetUserBlockedCommandResponse
        {
            Success = false,
            Message = "User not found"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserBlockedCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetBlocked(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserBlockedCommand>(cmd =>
            cmd.UserId == null &&
            cmd.IsBlocked == request.IsBlocked), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetHasVehicle_WithValidRequest_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserHasVehicleRequest
        {
            Id = Guid.NewGuid().ToString(),
            HasVehicle = true
        };

        var expectedResponse = new SetUserHasVehicleCommandResponse
        {
            Success = true,
            Message = "User vehicle status updated successfully"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserHasVehicleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);
        Assert.Equal(expectedResponse.Message, result.Message);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserHasVehicleCommand>(cmd =>
            cmd.UserId == Guid.Parse(request.Id) &&
            cmd.HasVehicle == request.HasVehicle), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetHasVehicle_WithEmptyId_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new SetUserHasVehicleRequest
        {
            Id = string.Empty,
            HasVehicle = false
        };

        var expectedResponse = new SetUserHasVehicleCommandResponse
        {
            Success = false,
            Message = "User not found"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserHasVehicleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserHasVehicleCommand>(cmd =>
            cmd.UserId == null &&
            cmd.HasVehicle == request.HasVehicle), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SetBlocked_WithDifferentBlockedValues_ShouldPassCorrectValue(bool isBlocked)
    {
        // Arrange
        var request = new SetUserBlockedRequest
        {
            Id = Guid.NewGuid().ToString(),
            IsBlocked = isBlocked
        };

        var expectedResponse = new SetUserBlockedCommandResponse
        {
            Success = true,
            Message = "User blocked status updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserBlockedCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetBlocked(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserBlockedCommand>(cmd =>
            cmd.IsBlocked == isBlocked), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SetHasVehicle_WithDifferentHasVehicleValues_ShouldPassCorrectValue(bool hasVehicle)
    {
        // Arrange
        var request = new SetUserHasVehicleRequest
        {
            Id = Guid.NewGuid().ToString(),
            HasVehicle = hasVehicle
        };

        var expectedResponse = new SetUserHasVehicleCommandResponse
        {
            Success = true,
            Message = "User vehicle status updated"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserHasVehicleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Success, result.Success);

        _mediatorMock.Verify(m => m.Send(It.Is<SetUserHasVehicleCommand>(cmd =>
            cmd.HasVehicle == hasVehicle), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SetLanguage_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var request = new SetUserLanguageRequest
        {
            Id = Guid.NewGuid().ToString(),
            Language = "fr"
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserLanguageCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.SetLanguage(request, _context));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task SetPhoneNumber_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var request = new SetUserPhoneNumberRequest
        {
            Id = Guid.NewGuid().ToString(),
            NewPhoneNumber = "+5551234567"
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserPhoneNumberCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.SetPhoneNumber(request, _context));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task SetBlocked_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var request = new SetUserBlockedRequest
        {
            Id = Guid.NewGuid().ToString(),
            IsBlocked = true
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserBlockedCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.SetBlocked(request, _context));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task SetHasVehicle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var request = new SetUserHasVehicleRequest
        {
            Id = Guid.NewGuid().ToString(),
            HasVehicle = true
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<SetUserHasVehicleCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.SetHasVehicle(request, _context));

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task CheckHasVehicle_WithValidUserId_ShouldReturnHasVehicleStatus()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = Guid.NewGuid().ToString(),
            TelegramId = string.Empty
        };

        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = true,
            UserExists = true,
            UserId = Guid.Parse(request.UserId),
            FoundBy = "UserId"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CheckHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.HasVehicle, result.HasVehicle);
        Assert.Equal(expectedResponse.UserExists, result.UserExists);
        Assert.Equal(expectedResponse.UserId?.ToString(), result.UserId);
        Assert.Equal(expectedResponse.FoundBy, result.FoundBy);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(cmd =>
            cmd.UserId == Guid.Parse(request.UserId) &&
            cmd.TelegramId == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicle_WithValidTelegramId_ShouldReturnHasVehicleStatus()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = string.Empty,
            TelegramId = "123456789"
        };

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
        var result = await _service.CheckHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.HasVehicle, result.HasVehicle);
        Assert.Equal(expectedResponse.UserExists, result.UserExists);
        Assert.Equal(expectedResponse.UserId?.ToString(), result.UserId);
        Assert.Equal(expectedResponse.FoundBy, result.FoundBy);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(cmd =>
            cmd.UserId == null &&
            cmd.TelegramId == long.Parse(request.TelegramId)), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicle_WithBothIds_ShouldPrioritizeUserId()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = Guid.NewGuid().ToString(),
            TelegramId = "987654321"
        };

        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = true,
            UserExists = true,
            UserId = Guid.Parse(request.UserId),
            FoundBy = "UserId"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CheckHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.HasVehicle, result.HasVehicle);
        Assert.Equal(expectedResponse.UserExists, result.UserExists);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(cmd =>
            cmd.UserId == Guid.Parse(request.UserId) &&
            cmd.TelegramId == long.Parse(request.TelegramId)), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicle_WithUserNotFound_ShouldReturnUserNotExists()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = Guid.NewGuid().ToString(),
            TelegramId = string.Empty
        };

        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = false,
            UserExists = false,
            UserId = null,
            FoundBy = string.Empty
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CheckHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.False(result.UserExists);
        Assert.Equal(string.Empty, result.UserId);
        Assert.Equal(string.Empty, result.FoundBy);
    }

    [Fact]
    public async Task CheckHasVehicle_WithEmptyIds_ShouldReturnUserNotExists()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = string.Empty,
            TelegramId = string.Empty
        };

        var expectedResponse = new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = false,
            UserExists = false,
            UserId = null,
            FoundBy = string.Empty
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CheckHasVehicle(request, _context);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.False(result.UserExists);

        _mediatorMock.Verify(m => m.Send(It.Is<CheckUserHasVehicleQuery>(cmd =>
            cmd.UserId == null &&
            cmd.TelegramId == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckHasVehicle_WhenMediatorThrowsException_ShouldPropagateException()
    {
        // Arrange
        var request = new CheckUserHasVehicleRequest
        {
            UserId = Guid.NewGuid().ToString(),
            TelegramId = string.Empty
        };

        var expectedException = new InvalidOperationException("Test exception");

        _mediatorMock.Setup(m => m.Send(It.IsAny<CheckUserHasVehicleQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.CheckHasVehicle(request, _context));

        Assert.Equal(expectedException.Message, exception.Message);
    }
} 