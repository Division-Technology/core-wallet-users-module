// <copyright file="SetUserHasVehicleCommandTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Xunit;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;

namespace Users.UnitTests.Domain.Commands.SetHasVehicle;

public class SetUserHasVehicleCommandTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var chatId = 123456789L;
        var telegramId = 987654321L;
        var phoneNumber = "+1234567890";
        var hasVehicle = true;

        // Act
        var command = new SetUserHasVehicleCommand
        {
            UserId = userId,
            ChatId = chatId,
            TelegramId = telegramId,
            PhoneNumber = phoneNumber,
            HasVehicle = hasVehicle
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(chatId, command.ChatId);
        Assert.Equal(telegramId, command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(hasVehicle, command.HasVehicle);
    }

    [Fact]
    public void Constructor_WithNullValues_ShouldSetProperties()
    {
        // Arrange & Act
        var command = new SetUserHasVehicleCommand
        {
            UserId = null,
            ChatId = null,
            TelegramId = null,
            PhoneNumber = null,
            HasVehicle = false
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.False(command.HasVehicle);
    }

    [Fact]
    public void Constructor_WithOnlyUserId_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var hasVehicle = true;

        // Act
        var command = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = hasVehicle
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(hasVehicle, command.HasVehicle);
    }

    [Fact]
    public void Constructor_WithOnlyPhoneNumber_ShouldSetProperties()
    {
        // Arrange
        var phoneNumber = "+1234567890";
        var hasVehicle = false;

        // Act
        var command = new SetUserHasVehicleCommand
        {
            PhoneNumber = phoneNumber,
            HasVehicle = hasVehicle
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(hasVehicle, command.HasVehicle);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_WithDifferentHasVehicleValues_ShouldSetProperties(bool hasVehicle)
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var command = new SetUserHasVehicleCommand
        {
            UserId = userId,
            HasVehicle = hasVehicle
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(hasVehicle, command.HasVehicle);
    }
} 