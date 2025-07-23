// <copyright file="GetUserClientByIdQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Users.Domain.Enums;

namespace Users.Domain.Entities.UserClients.Queries.GetById;

public class GetUserClientByIdQueryResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? ClientId { get; set; }

    public string? DeviceId { get; set; }

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