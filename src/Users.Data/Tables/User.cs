// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using Users.Domain.Enums;

namespace Users.Data.Tables;

public class User : BaseEntity
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Language { get; set; }

    public string? PhoneNumber { get; set; }

    public Guid? Referrer { get; set; } // FK to User.Id

    public RegistrationStatus RegistrationStatus { get; set; } // Profile completion state

    public bool IsBlock { get; set; } // Admin-initiated block

    public bool IsAdmin { get; set; }

    public bool IsSuspicious { get; set; }

    public bool IsPremium { get; set; }

    public bool HasVehicle { get; set; }

    // created_at and updated_at are inherited from BaseEntity as CreatedAt and ModifiedAt
}