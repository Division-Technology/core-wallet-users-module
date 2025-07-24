// <copyright file="CheckUserHasVehicleQueryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Xunit;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;

namespace Users.UnitTests.Domain.Queries.CheckHasVehicle;

public class CheckUserHasVehicleQueryTests
{
    [Fact]
    public void Constructor_WithUserId_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var query = new CheckUserHasVehicleQuery(userId: userId);

        // Assert
        Assert.Equal(userId, query.UserId);
        Assert.Null(query.TelegramId);
    }

    [Fact]
    public void Constructor_WithTelegramId_ShouldSetProperties()
    {
        // Arrange
        var telegramId = 123456789L;

        // Act
        var query = new CheckUserHasVehicleQuery(telegramId: telegramId);

        // Assert
        Assert.Null(query.UserId);
        Assert.Equal(telegramId, query.TelegramId);
    }

    [Fact]
    public void Constructor_WithBothIds_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var telegramId = 987654321L;

        // Act
        var query = new CheckUserHasVehicleQuery(userId, telegramId);

        // Assert
        Assert.Equal(userId, query.UserId);
        Assert.Equal(telegramId, query.TelegramId);
    }

    [Fact]
    public void Constructor_WithNoIds_ShouldSetProperties()
    {
        // Arrange & Act
        var query = new CheckUserHasVehicleQuery();

        // Assert
        Assert.Null(query.UserId);
        Assert.Null(query.TelegramId);
    }

    [Fact]
    public void Constructor_WithNullValues_ShouldSetProperties()
    {
        // Arrange & Act
        var query = new CheckUserHasVehicleQuery(null, null);

        // Assert
        Assert.Null(query.UserId);
        Assert.Null(query.TelegramId);
    }
} 