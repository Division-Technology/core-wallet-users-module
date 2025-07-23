// <copyright file="GetUserClientByIdQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.UserClients.Queries.GetById;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Queries.GetById;

public class GetUserClientByIdQueryHandler : IRequestHandler<GetUserClientByIdQuery, GetUserClientByIdQueryResponse>
{
    private readonly IUserClientsRepository repository;
    private readonly IMapper mapper;

    public GetUserClientByIdQueryHandler(IUserClientsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetUserClientByIdQueryResponse> Handle(GetUserClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await this.repository.GetAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"UserClient {request.Id} not found.");
        return this.mapper.Map<GetUserClientByIdQueryResponse>(client);
    }
}