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

    public ClientType Type { get; set; }

    public string? ClientData { get; set; }
}