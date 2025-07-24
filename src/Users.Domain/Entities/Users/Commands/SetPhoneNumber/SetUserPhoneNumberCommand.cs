// <copyright file="SetUserPhoneNumberCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Commands.SetPhoneNumber;

public class SetUserPhoneNumberCommand : IRequest<SetUserPhoneNumberCommandResponse>
{
    // Multiple identifier options
    public Guid? UserId { get; set; }
    public long? ChatId { get; set; }
    public long? TelegramId { get; set; }
    public string? PhoneNumber { get; set; }
    
    // The new phone number to set
    public string? NewPhoneNumber { get; set; }
} 