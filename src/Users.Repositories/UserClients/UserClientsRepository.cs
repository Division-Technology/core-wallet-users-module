// <copyright file="UserClientsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Data;
using Users.Data.Tables;
using Users.Domain.Enums;

namespace Users.Repositories.UserClients;

public class UserClientsRepository : IUserClientsRepository
{
    private readonly UsersDbContext dbContext;

    public UserClientsRepository(UsersDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Task<UserClient?> GetAsync(Func<UserClient, bool> predicate, CancellationToken cancellationToken = default)
        => Task.FromResult(this.dbContext.UserClients.AsEnumerable().FirstOrDefault(predicate));

    /// <inheritdoc/>
    public async Task<IEnumerable<UserClient>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await this.dbContext.UserClients.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<UserClient?> GetByUserIdAndTypeAsync(Guid userId, ChannelType type, CancellationToken cancellationToken = default)
        => await this.dbContext.UserClients.FirstOrDefaultAsync(x => x.UserId == userId && x.ChannelType == type, cancellationToken);

    /// <inheritdoc/>
    public async Task<UserClient?> GetByTelegramIdAsync(string telegramId, CancellationToken cancellationToken = default)
        => await this.dbContext.UserClients.FirstOrDefaultAsync(x => x.TelegramId == telegramId, cancellationToken);

    /// <inheritdoc/>
    public async Task AddAsync(UserClient userClient, CancellationToken cancellationToken = default)
    {
        this.dbContext.UserClients.Add(userClient);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public void Update(UserClient userClient)
    {
        this.dbContext.UserClients.Update(userClient);
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserClient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.dbContext.UserClients.ToListAsync(cancellationToken);
    }
}