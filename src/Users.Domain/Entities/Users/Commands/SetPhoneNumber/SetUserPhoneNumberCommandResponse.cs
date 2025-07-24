// <copyright file="SetUserPhoneNumberCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.Users.Commands.SetPhoneNumber;

public class SetUserPhoneNumberCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
} 