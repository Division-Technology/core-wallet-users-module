// <copyright file="PatchUpdateUserClientCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Users.Domain.Entities.UserClients.Commands.PatchUpdate;

public class PatchUpdateUserClientCommandResponse
{
    public Guid Id { get; set; }

    public bool Success { get; set; }

    public string? Message { get; set; }

    // Optionally, return updated fields
}