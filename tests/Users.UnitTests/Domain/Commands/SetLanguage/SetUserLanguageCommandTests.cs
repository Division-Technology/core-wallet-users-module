// <copyright file="SetUserLanguageCommandTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Xunit;
using Users.Domain.Entities.Users.Commands.SetLanguage;

namespace Users.UnitTests.Domain.Commands.SetLanguage;

public class SetUserLanguageCommandTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var chatId = 123456789L;
        var telegramId = 987654321L;
        var phoneNumber = "+1234567890";
        var language = "en";

        // Act
        var command = new SetUserLanguageCommand
        {
            UserId = userId,
            ChatId = chatId,
            TelegramId = telegramId,
            PhoneNumber = phoneNumber,
            Language = language
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Equal(chatId, command.ChatId);
        Assert.Equal(telegramId, command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(language, command.Language);
    }

    [Fact]
    public void Constructor_WithNullValues_ShouldSetProperties()
    {
        // Arrange & Act
        var command = new SetUserLanguageCommand
        {
            UserId = null,
            ChatId = null,
            TelegramId = null,
            PhoneNumber = null,
            Language = string.Empty
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(string.Empty, command.Language);
    }

    [Fact]
    public void Constructor_WithOnlyUserId_ShouldSetProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var language = "es";

        // Act
        var command = new SetUserLanguageCommand
        {
            UserId = userId,
            Language = language
        };

        // Assert
        Assert.Equal(userId, command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Null(command.PhoneNumber);
        Assert.Equal(language, command.Language);
    }

    [Fact]
    public void Constructor_WithOnlyPhoneNumber_ShouldSetProperties()
    {
        // Arrange
        var phoneNumber = "+9876543210";
        var language = "fr";

        // Act
        var command = new SetUserLanguageCommand
        {
            PhoneNumber = phoneNumber,
            Language = language
        };

        // Assert
        Assert.Null(command.UserId);
        Assert.Null(command.ChatId);
        Assert.Null(command.TelegramId);
        Assert.Equal(phoneNumber, command.PhoneNumber);
        Assert.Equal(language, command.Language);
    }
} 