// <copyright file="GetUserClientByTelegramIdQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.UserClients.Queries.GetByTelegramId;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Queries.GetByTelegramId;

public class GetUserClientByTelegramIdQueryHandler : IRequestHandler<GetUserClientByTelegramIdQuery, GetUserClientByTelegramIdQueryResponse>
{
    private readonly IUserClientsRepository repository;
    private readonly IMapper mapper;

    public GetUserClientByTelegramIdQueryHandler(IUserClientsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetUserClientByTelegramIdQueryResponse> Handle(GetUserClientByTelegramIdQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TelegramId))
        {
            throw new NotFoundException("TelegramId must be provided.");
        }

        var client = await this.repository.GetByTelegramIdAsync(request.TelegramId, cancellationToken)
            ?? throw new NotFoundException($"UserClient with TelegramId {request.TelegramId} not found.");
        return this.mapper.Map<GetUserClientByTelegramIdQueryResponse>(client);
    }
}