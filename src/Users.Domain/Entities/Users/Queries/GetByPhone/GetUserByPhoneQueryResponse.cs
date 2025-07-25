// <copyright file="GetUserByPhoneQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.GetByPhone;

public class GetUserByPhoneQueryResponse
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Language { get; set; }

    public string? PhoneNumber { get; set; }

    public Guid? Referrer { get; set; }

    public RegistrationStatus RegistrationStatus { get; set; }

    public bool IsBlock { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsSuspicious { get; set; }

    public bool IsPremium { get; set; }

    public bool HasVehicle { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}