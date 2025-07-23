// <copyright file="UserExistsQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Users.Domain.Entities.Users.Queries.Exists;

public class UserExistsQueryResponse
{
    public bool Exists { get; set; }

    public Guid? UserId { get; set; }

    public string? FoundBy { get; set; }
}