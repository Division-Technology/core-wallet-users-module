// <copyright file="ListUsersQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.ListUsers;

public class ListUsersQuery : IRequest<ListUsersQueryResponse>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    // Add filters as needed
}