// <copyright file="ListUsersQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries.ListUsers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, ListUsersQueryResponse>
{
    private readonly IUsersRepository repository;
    private readonly IMapper mapper;

    public ListUsersQueryHandler(IUsersRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<ListUsersQueryResponse> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await this.repository.GetAllAsync(cancellationToken);
        var paged = users.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
        return new ListUsersQueryResponse
        {
            TotalCount = users.Count(),
            Users = paged.Select(u => this.mapper.Map<UserListItemDto>(u)).ToList(),
        };
    }
}