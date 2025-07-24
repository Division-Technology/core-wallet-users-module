// <copyright file="ListUsersQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.ListUsers;

public class ListUsersQueryResponse
{
    public int TotalCount { get; set; }

    public List<UserListItemDto> Users { get; set; } = new();
}

public class UserListItemDto
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public RegistrationStatus RegistrationStatus { get; set; }

    public bool HasVehicle { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime CreatedAt { get; set; }
}