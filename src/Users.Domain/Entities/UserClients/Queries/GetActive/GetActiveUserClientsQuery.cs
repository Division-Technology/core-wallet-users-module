// <copyright file="GetActiveUserClientsQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.UserClients.Queries.GetActive;

public class GetActiveUserClientsQuery : IRequest<GetActiveUserClientsQueryResponse>
{
    public Guid UserId { get; set; }
}