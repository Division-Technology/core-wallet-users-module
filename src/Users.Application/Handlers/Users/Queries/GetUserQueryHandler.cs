// <copyright file="GetUserQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Queries.GetUser;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
{
    private readonly IUsersRepository repository;
    private readonly IMapper mapper;

    public GetUserQueryHandler(IUsersRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = request.Id.HasValue
            ? await this.repository.GetAsync(x => x.Id == request.Id.Value, cancellationToken)
            : !string.IsNullOrEmpty(request.PhoneNumber)
                ? await this.repository.GetAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken)
                : null;
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return this.mapper.Map<GetUserQueryResponse>(user);
    }
}