// <copyright file="SetUserBlockedCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.Users.Commands.SetBlocked;

public class SetUserBlockedCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
} 