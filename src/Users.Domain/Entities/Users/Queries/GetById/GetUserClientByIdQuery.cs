// <copyright file="GetUserClientByIdQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetById;

public class GetUserClientByIdQuery : IRequest<GetUserClientByIdQueryResponse>
{
    public Guid Id { get; set; }

    public GetUserClientByIdQuery(Guid id)
    {
        this.Id = id;
    }
}

public class GetUserClientByIdQueryResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? ClientId { get; set; }

    public string? DeviceId { get; set; }

    public string? Platform { get; set; }

    public string? Language { get; set; }

    public string? Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public bool IsActive { get; set; }
}