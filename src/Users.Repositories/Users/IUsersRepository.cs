// <copyright file="IUsersRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Data.Tables;
using Users.Repositories.Common;

namespace Users.Repositories.Users;

public interface IUsersRepository : IGenericRepository<User>
{
    Task<User?> GetAsync(Func<User, bool> predicate, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<User?> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default);
    
    Task<User?> GetByChatIdAsync(long chatId, CancellationToken cancellationToken = default);
}