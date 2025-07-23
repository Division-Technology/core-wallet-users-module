// <copyright file="UserExistsQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.Exists;

public class UserExistsQuery : IRequest<UserExistsQueryResponse>
{
    public string? TelegramId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ChatId { get; set; }

    public string? UserId { get; set; }

    public UserExistsQuery(string? telegramId = null, string? phoneNumber = null, string? chatId = null, string? userId = null)
    {
        this.TelegramId = telegramId;
        this.PhoneNumber = phoneNumber;
        this.ChatId = chatId;
        this.UserId = userId;
    }
}