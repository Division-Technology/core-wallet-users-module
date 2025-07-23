// <copyright file="GetUserClientByIdQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.UserClients.Queries.GetById;

public class GetUserClientByIdQuery : IRequest<GetUserClientByIdQueryResponse>
{
    public Guid Id { get; set; }

    public GetUserClientByIdQuery(Guid id)
    {
        this.Id = id;
    }
}