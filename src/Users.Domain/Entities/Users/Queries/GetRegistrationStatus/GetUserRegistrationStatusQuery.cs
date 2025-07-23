// <copyright file="GetUserRegistrationStatusQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetRegistrationStatus;

public class GetUserRegistrationStatusQuery : IRequest<GetUserRegistrationStatusQueryResponse>
{
    public Guid UserId { get; set; }
}