// <copyright file="SetUserBlockedCommandTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Xunit;
using Users.Domain.Entities.Users.Commands.SetBlocked;

namespace Users.UnitTests.Domain.Commands.SetBlocked;

public class SetUserBlockedCommandTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var chatId = 123456789L;
        var telegramId = 987654321L;
        var phoneNumber = "+1234567890";
        var isBlocked = true;

        // Act
        var command = new SetUserBlockedCommand
        {
            UserId = userId,
            ChatId = chatId,
            TelegramId = telegramId,
            PhoneNumber = phoneNumber,
            IsBlocked = isBlocked
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(chatId, command.ChatId);
        Assert.Equal(telegramId, command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(isBlocked, command.IsBlocked);
    }

    [Fact]
    public void Constructor_WithNullValues_ShouldSetProperties()
    {
        // Arrange & Act
        var command = new SetUserBlockedCommand
        {
            UserId = null,
            ChatId = null,
            TelegramId = null,
            PhoneNumber = null,
            IsBlocked = false
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.False(command.IsBlocked);
    }

    [Fact]
    public void Constructor_WithOnlyUserId_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var isBlocked = true;

        // Act
        var command = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = isBlocked
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(isBlocked, command.IsBlocked);
    }

    [Fact]
    public void Constructor_WithOnlyPhoneNumber_ShouldSetProperties()
    {
        // Arrange
        var phoneNumber = "+1234567890";
        var isBlocked = false;

        // Act
        var command = new SetUserBlockedCommand
        {
            PhoneNumber = phoneNumber,
            IsBlocked = isBlocked
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(isBlocked, command.IsBlocked);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_WithDifferentBlockedValues_ShouldSetProperties(bool isBlocked)
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var command = new SetUserBlockedCommand
        {
            UserId = userId,
            IsBlocked = isBlocked
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(isBlocked, command.IsBlocked);
    }
} 