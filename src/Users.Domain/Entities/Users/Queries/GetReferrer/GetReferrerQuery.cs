// <copyright file="GetReferrerQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetReferrer;

public class GetReferrerQuery : IRequest<GetReferrerQueryResponse>
{
    public Guid UserId { get; set; }
}