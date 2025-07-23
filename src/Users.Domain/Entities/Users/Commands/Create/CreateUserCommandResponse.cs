// <copyright file="CreateUserCommandResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Commands.Create;

public class CreateUserCommandResponse
{
    public Guid Id { get; set; }

    public RegistrationStatus RegistrationStatus { get; set; }
}