// <copyright file="GetUserClientByTelegramIdQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Users.Domain.Entities.UserClients.Queries.GetByTelegramId;

public class GetUserClientByTelegramIdQueryResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? TelegramId { get; set; }

    public string? ChatId { get; set; }

    public string? Username { get; set; }

    public bool IsBlocked { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }
}