// <copyright file="GetUserClientsQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.UserClients.Queries.GetByUser;

public class GetUserClientsQuery : IRequest<GetUserClientsQueryResponse>
{
    public Guid UserId { get; set; }
}