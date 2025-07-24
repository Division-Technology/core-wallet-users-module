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
using Users.Data.Tables;

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
        User? user = null;

        // Try to find user by different identifiers in order of priority
        if (request.Id.HasValue)
        {
            user = await this.repository.GetByIdAsync(request.Id.Value, cancellationToken);
        }
        else if (request.TelegramId.HasValue)
        {
            user = await this.repository.GetByTelegramIdAsync(request.TelegramId.Value, cancellationToken);
        }
        else if (request.ChatId.HasValue)
        {
            user = await this.repository.GetByChatIdAsync(request.ChatId.Value, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            user = await this.repository.GetAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);
        }

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return this.mapper.Map<GetUserQueryResponse>(user);
    }
}