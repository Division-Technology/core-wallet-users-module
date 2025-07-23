// <copyright file="GetUserClientsQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Domain.Entities.UserClients.Queries.GetByUser;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Queries.GetByUser;

public class GetUserClientsQueryHandler : IRequestHandler<GetUserClientsQuery, GetUserClientsQueryResponse>
{
    private readonly IUserClientsRepository repository;
    private readonly IMapper mapper;

    public GetUserClientsQueryHandler(IUserClientsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetUserClientsQueryResponse> Handle(GetUserClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await this.repository.GetAllByUserIdAsync(request.UserId, cancellationToken);
        return new GetUserClientsQueryResponse
        {
            Clients = clients.Select(c => this.mapper.Map<UserClientDto>(c)).ToList(),
        };
    }
}