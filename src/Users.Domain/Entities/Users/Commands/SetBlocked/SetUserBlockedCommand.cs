// <copyright file="SetUserBlockedCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Commands.SetBlocked;

public class SetUserBlockedCommand : IRequest<SetUserBlockedCommandResponse>
{
    // Multiple identifier options
    public Guid? UserId { get; set; }
    public long? ChatId { get; set; }
    public long? TelegramId { get; set; }
    public string? PhoneNumber { get; set; }
    
    // The blocked status to set
    public bool IsBlocked { get; set; }
} 