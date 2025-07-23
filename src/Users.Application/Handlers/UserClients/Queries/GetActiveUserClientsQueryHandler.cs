// <copyright file="GetActiveUserClientsQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Domain.Entities.UserClients.Queries.GetActive;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Queries.GetActive;

public class GetActiveUserClientsQueryHandler : IRequestHandler<GetActiveUserClientsQuery, GetActiveUserClientsQueryResponse>
{
    private readonly IUserClientsRepository repository;
    private readonly IMapper mapper;

    public GetActiveUserClientsQueryHandler(IUserClientsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetActiveUserClientsQueryResponse> Handle(GetActiveUserClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await this.repository.GetAllByUserIdAsync(request.UserId, cancellationToken);
        var active = clients.Where(c => c.IsActive).ToList();
        return new GetActiveUserClientsQueryResponse
        {
            Clients = active.Select(c => this.mapper.Map<UserClientDto>(c)).ToList(),
        };
    }
}