// <copyright file="SetUserHasVehicleCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Commands.SetHasVehicle;

public class SetUserHasVehicleCommand : IRequest<SetUserHasVehicleCommandResponse>
{
    // Multiple identifier options
    public Guid? UserId { get; set; }
    public long? ChatId { get; set; }
    public long? TelegramId { get; set; }
    public string? PhoneNumber { get; set; }
    
    // The has vehicle status to set
    public bool HasVehicle { get; set; }
} 