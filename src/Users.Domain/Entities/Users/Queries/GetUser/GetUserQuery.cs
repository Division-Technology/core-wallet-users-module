// <copyright file="GetUserQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetUser;

public class GetUserQuery : IRequest<GetUserQueryResponse>
{
    public Guid? Id { get; set; }

    public string? PhoneNumber { get; set; }

    public long? TelegramId { get; set; }

    public long? ChatId { get; set; }

    // Add other identifiers as needed (e.g., email, etc.)
}