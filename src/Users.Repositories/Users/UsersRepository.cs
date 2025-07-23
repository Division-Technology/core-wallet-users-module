// <copyright file="UsersRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Data;
using Users.Data.Tables;
using Users.Repositories.Common;

namespace Users.Repositories.Users;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(UsersDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc/>
    public Task<User?> GetAsync(Func<User, bool> predicate, CancellationToken cancellationToken = default)
        => Task.FromResult(this.DbContext.Users.AsEnumerable().FirstOrDefault(predicate));

    /// <inheritdoc/>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.DbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}