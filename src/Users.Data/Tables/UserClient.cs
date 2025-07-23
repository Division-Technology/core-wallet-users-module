// <copyright file="UserClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Text.Json;
using Users.Domain.Enums;

namespace Users.Data.Tables;

public class UserClient
{
    public Guid Id { get; set; } // Primary Key

    public Guid UserId { get; set; } // FK to User

    public ChannelType ChannelType { get; set; }
    public string? TelegramId { get; set; }
    public string? ChatId { get; set; }
    public string? DeviceToken { get; set; }
    public string? SessionId { get; set; }
    public string? Platform { get; set; }
    public string? Version { get; set; }
    public string? Language { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastSeenAt { get; set; }
}