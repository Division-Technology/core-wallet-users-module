// <copyright file="IGenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;

namespace Users.Repositories.Common;

public interface IGenericRepository<T>
    where T : class
{
    T? Get(Expression<Func<T, bool>> expression);

    IEnumerable<T> GetAll();

    IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);

    IEnumerable<T> GetAll(int skip, int take);

    void Add(T entity);

    Task AddAndSaveAsync(T entity);

    void AddRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    int Count();

    Task<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    public IQueryable<T> GetAllAsQueryable();

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}