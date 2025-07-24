// <copyright file="SetUserHasVehicleCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.Users.Commands.SetHasVehicle;

public class SetUserHasVehicleCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
} 