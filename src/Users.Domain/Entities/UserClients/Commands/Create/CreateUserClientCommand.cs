// <copyright file="CreateUserClientCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Text.Json;
using MediatR;
using Users.Domain.Enums;

namespace Users.Domain.Entities.UserClients.Commands.Create;

public class CreateUserClientCommand : IRequest<CreateUserClientCommandResponse>
{
    public Guid UserId { get; set; }

    public ChannelType ChannelType { get; set; }
    public string? TelegramId { get; set; }
    public string? ChatId { get; set; }
    public string? DeviceToken { get; set; }
    public string? SessionId { get; set; }
    public string? Platform { get; set; }
    public string? Version { get; set; }
    public string? Language { get; set; }
}