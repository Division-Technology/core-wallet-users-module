// <copyright file="SetUserLanguageCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.Users.Commands.SetLanguage;

public class SetUserLanguageCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
} 