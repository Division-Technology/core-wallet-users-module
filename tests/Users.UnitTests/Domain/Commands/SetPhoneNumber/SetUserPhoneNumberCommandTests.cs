// <copyright file="SetUserPhoneNumberCommandTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Xunit;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;

namespace Users.UnitTests.Domain.Commands.SetPhoneNumber;

public class SetUserPhoneNumberCommandTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var chatId = 123456789L;
        var telegramId = 987654321L;
        var phoneNumber = "+1234567890";
        var newPhoneNumber = "+9876543210";

        // Act
        var command = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            ChatId = chatId,
            TelegramId = telegramId,
            PhoneNumber = phoneNumber,
            NewPhoneNumber = newPhoneNumber
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(chatId, command.ChatId);
        Assert.Equal(telegramId, command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(newPhoneNumber, command.NewPhoneNumber);
    }

    [Fact]
    public void Constructor_WithNullValues_ShouldSetProperties()
    {
        // Arrange & Act
        var command = new SetUserPhoneNumberCommand
        {
            UserId = null,
            ChatId = null,
            TelegramId = null,
            PhoneNumber = null,
            NewPhoneNumber = string.Empty
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(string.Empty, command.NewPhoneNumber);
    }

    [Fact]
    public void Constructor_WithOnlyUserId_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var newPhoneNumber = "+5551234567";

        // Act
        var command = new SetUserPhoneNumberCommand
        {
            UserId = userId,
            NewPhoneNumber = newPhoneNumber
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(newPhoneNumber, command.NewPhoneNumber);
    }

    [Fact]
    public void Constructor_WithOnlyPhoneNumber_ShouldSetProperties()
    {
        // Arrange
        var phoneNumber = "+1234567890";
        var newPhoneNumber = "+9876543210";

        // Act
        var command = new SetUserPhoneNumberCommand
        {
            PhoneNumber = phoneNumber,
            NewPhoneNumber = newPhoneNumber
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(newPhoneNumber, command.NewPhoneNumber);
    }
} 