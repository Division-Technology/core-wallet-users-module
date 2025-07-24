// <copyright file="CheckUserHasVehicleQueryHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Users.Application.Handlers.Users.Queries;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;
using Users.Data.Tables;
using Users.Repositories.Users;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries;

public class CheckUserHasVehicleQueryHandlerTests
{
    private readonly Mock<IUsersRepository> _repositoryMock;
    private readonly CheckUserHasVehicleQueryHandler _handler;

    public CheckUserHasVehicleQueryHandlerTests()
    {
        _repositoryMock = new Mock<IUsersRepository>();
        _handler = new CheckUserHasVehicleQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidUserId_ShouldReturnHasVehicleStatus()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CheckUserHasVehicleQuery(userId: userId);

        var user = new User
        {
            Id = userId,
            HasVehicle = true,
            FirstName = "Test",
            LastName = "User"
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasVehicle);
        Assert.True(result.UserExists);
        Assert.Equal(userId, result.UserId);
        Assert.Equal("UserId", result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithValidTelegramId_ShouldReturnHasVehicleStatus()
    {
        // Arrange
        var telegramId = 123456789L;
        var userId = Guid.NewGuid();
        var request = new CheckUserHasVehicleQuery(telegramId: telegramId);

        var user = new User
        {
            Id = userId,
            TelegramId = telegramId,
            HasVehicle = false,
            FirstName = "Test",
            LastName = "User"
        };

        _repositoryMock.Setup(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.True(result.UserExists);
        Assert.Equal(userId, result.UserId);
        Assert.Equal("TelegramId", result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithBothIds_ShouldPrioritizeUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var telegramId = 123456789L;
        var request = new CheckUserHasVehicleQuery(userId, telegramId);

        var user = new User
        {
            Id = userId,
            TelegramId = telegramId,
            HasVehicle = true,
            FirstName = "Test",
            LastName = "User"
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasVehicle);
        Assert.True(result.UserExists);
        Assert.Equal(userId, result.UserId);
        Assert.Equal("UserId", result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithUserIdNotFound_ShouldReturnUserNotExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CheckUserHasVehicleQuery(userId: userId);

        _repositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.False(result.UserExists);
        Assert.Null(result.UserId);
        Assert.Equal(string.Empty, result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithTelegramIdNotFound_ShouldReturnUserNotExists()
    {
        // Arrange
        var telegramId = 123456789L;
        var request = new CheckUserHasVehicleQuery(telegramId: telegramId);

        _repositoryMock.Setup(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.False(result.UserExists);
        Assert.Null(result.UserId);
        Assert.Equal(string.Empty, result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoIds_ShouldReturnUserNotExists()
    {
        // Arrange
        var request = new CheckUserHasVehicleQuery();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasVehicle);
        Assert.False(result.UserExists);
        Assert.Null(result.UserId);
        Assert.Equal(string.Empty, result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithUserIdNotFoundButTelegramIdFound_ShouldReturnTelegramIdResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var telegramId = 123456789L;
        var foundUserId = Guid.NewGuid();
        var request = new CheckUserHasVehicleQuery(userId, telegramId);

        var user = new User
        {
            Id = foundUserId,
            TelegramId = telegramId,
            HasVehicle = true,
            FirstName = "Test",
            LastName = "User"
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);
        _repositoryMock.Setup(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasVehicle);
        Assert.True(result.UserExists);
        Assert.Equal(foundUserId, result.UserId);
        Assert.Equal("TelegramId", result.FoundBy);

        _repositoryMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithCancellationToken_ShouldPassCancellationToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CheckUserHasVehicleQuery(userId: userId);
        var cancellationToken = new CancellationToken(); // Regular token

        var user = new User
        {
            Id = userId,
            HasVehicle = true,
            FirstName = "Test",
            LastName = "User"
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        _repositoryMock.Verify(r => r.GetByIdAsync(userId, cancellationToken), Times.Once);
    }
} 