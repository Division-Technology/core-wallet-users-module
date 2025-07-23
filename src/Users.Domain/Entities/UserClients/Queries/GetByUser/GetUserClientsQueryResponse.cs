// <copyright file="GetUserClientsQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Users.Domain.Enums;

namespace Users.Domain.Entities.UserClients.Queries.GetByUser;

public class GetUserClientsQueryResponse
{
    public List<UserClientDto> Clients { get; set; } = new ();
}

public class UserClientDto
{
    public Guid Id { get; set; }
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