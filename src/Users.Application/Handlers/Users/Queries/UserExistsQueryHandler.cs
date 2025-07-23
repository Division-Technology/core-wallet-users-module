// <copyright file="UserExistsQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries;

public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, UserExistsQueryResponse>
{
    private readonly IUsersRepository repository;

    public UserExistsQueryHandler(IUsersRepository repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task<UserExistsQueryResponse> Handle(UserExistsQuery request, CancellationToken cancellationToken)
    {
        // Check by UserId first (most specific)
        if (!string.IsNullOrEmpty(request.UserId))
        {
            if (Guid.TryParse(request.UserId, out var userIdGuid))
            {
                var user = await this.repository.GetAsync(x => x.Id == userIdGuid, cancellationToken);
                if (user != null)
                {
                    return new UserExistsQueryResponse { Exists = true, FoundBy = "UserId" };
                }
            }
        }

        // Check by TelegramId (now on User)
        if (!string.IsNullOrEmpty(request.TelegramId))
        {
            var user = await this.repository.GetAsync(x => x.TelegramId.ToString() == request.TelegramId, cancellationToken);
            if (user != null)
            {
                return new UserExistsQueryResponse { Exists = true, FoundBy = "TelegramId" };
            }
        }

        // Check by PhoneNumber
        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            var user = await this.repository.GetAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (user != null)
            {
                return new UserExistsQueryResponse
                {
                    Exists = true,
                    UserId = user.Id,
                    FoundBy = "PhoneNumber",
                };
            }
        }

        // Check by ChatId (now on User)
        if (!string.IsNullOrEmpty(request.ChatId))
        {
            var user = await this.repository.GetAsync(x => x.ChatId.ToString() == request.ChatId, cancellationToken);
            if (user != null)
            {
                return new UserExistsQueryResponse { Exists = true, FoundBy = "ChatId" };
            }
        }

        return new UserExistsQueryResponse
        {
            Exists = false,
            UserId = null,
            FoundBy = null,
        };
    }
}