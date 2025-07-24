// <copyright file="GetUserQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.GetUser;

public class GetUserQueryResponse
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Language { get; set; }

    public bool IsBlocked { get; set; }

    public bool HasVehicle { get; set; }

    // Telegram-related fields
    public long? TelegramId { get; set; }
    public long? ChatId { get; set; }
    public string? Username { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}