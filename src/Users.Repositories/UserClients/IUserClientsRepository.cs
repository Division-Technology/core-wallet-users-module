// <copyright file="IUserClientsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Data.Tables;
using Users.Domain.Enums;

namespace Users.Repositories.UserClients;

public interface IUserClientsRepository
{
    Task<UserClient?> GetAsync(Func<UserClient, bool> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserClient>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<UserClient?> GetByUserIdAndTypeAsync(Guid userId, ChannelType type, CancellationToken cancellationToken = default);

    Task<UserClient?> GetByTelegramIdAsync(string telegramId, CancellationToken cancellationToken = default);

    Task AddAsync(UserClient userClient, CancellationToken cancellationToken = default);

    void Update(UserClient userClient);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UserClient>> GetAllAsync(CancellationToken cancellationToken = default);
}