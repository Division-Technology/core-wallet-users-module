// <copyright file="GetUserClientsQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Users.Domain.Entities.UserClients.Queries.GetByUser;

public class GetUserClientsQueryResponse
{
    public List<UserClientDto> Clients { get; set; } = new ();
}

public class UserClientDto
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }
}