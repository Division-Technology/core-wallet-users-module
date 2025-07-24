// <copyright file="SetUserLanguageCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Commands.SetLanguage;

public class SetUserLanguageCommand : IRequest<SetUserLanguageCommandResponse>
{
    // Multiple identifier options
    public Guid? UserId { get; set; }
    public long? ChatId { get; set; }
    public long? TelegramId { get; set; }
    public string? PhoneNumber { get; set; }
    
    // The language to set
    public string? Language { get; set; }
} 