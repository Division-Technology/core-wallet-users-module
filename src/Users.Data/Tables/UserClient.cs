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

    public ClientType Type { get; set; } // Type of client (Telegram, MobileApp, etc.)

    public string? ClientData { get; set; } // Flexible storage for type-specific data

    public DateTime CreatedAt { get; set; } // First seen time

    public DateTime? LastSeenAt { get; set; } // Most recent activity

    public bool IsActive { get; set; } // Soft session activity flag
}