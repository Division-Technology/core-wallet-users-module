// <copyright file="CreateUserClientCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Users.Domain.Entities.UserClients.Commands.Create;

public class CreateUserClientCommandResponse
{
    public Guid Id { get; set; }

    public bool IsNew { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }
}