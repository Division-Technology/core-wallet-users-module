// <copyright file="CreateUserCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Commands.Create;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Language { get; set; }

    public string? PhoneNumber { get; set; }

    public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Unregistered;

    public bool IsBlocked { get; set; } // Renamed from IsBlock

    public bool HasVehicle { get; set; }

    // Telegram-related fields
    public long? TelegramId { get; set; }
    public long? ChatId { get; set; }
    public string? Username { get; set; }
}
