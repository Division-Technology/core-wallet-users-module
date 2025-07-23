// <copyright file="PatchUpdateUserClientCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.UserClients.Commands.PatchUpdate;

public class PatchUpdateUserClientCommand : IRequest<PatchUpdateUserClientCommandResponse>
{
    public Guid Id { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsBlocked { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public string? Platform { get; set; }

    public string? Language { get; set; }

    public string? Version { get; set; }

    // Add any other patchable fields here
}