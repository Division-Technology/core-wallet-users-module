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

    public RegistrationStatus RegistrationStatus { get; set; }

    public bool IsBlocked { get; set; }

    public bool HasVehicle { get; set; }

    // Telegram-related fields
    public long? TelegramId { get; set; }
    public long? ChatId { get; set; }
    public string? Username { get; set; }

    // created_at and updated_at are inherited from BaseEntity as CreatedAt and ModifiedAt
}