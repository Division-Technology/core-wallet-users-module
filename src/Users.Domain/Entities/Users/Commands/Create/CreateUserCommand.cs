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

    public Guid? Referrer { get; set; }

    public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Unregistered;

    public bool IsBlock { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsSuspicious { get; set; }

    public bool IsPremium { get; set; }

    public bool HasVehicle { get; set; }
}
