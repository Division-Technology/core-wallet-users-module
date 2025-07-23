// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Users.Data;
using Users.Data.Tables;

namespace Users.Repositories.Common;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly UsersDbContext DbContext;
    protected readonly DbSet<T> EntitySet;

    public GenericRepository(UsersDbContext dbContext)
    {
        this.DbContext = dbContext;
        this.EntitySet = this.DbContext.Set<T>();
    }

    /// <inheritdoc/>
    public void Add(T entity)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.CreatedAt = DateTime.UtcNow;
            baseEntity.ModifiedAt = DateTime.UtcNow;
        }

        this.DbContext.Add(entity);
    }

    /// <inheritdoc/>
    public async Task AddAndSaveAsync(T entity)
    {
        this.Add(entity);
        await this.DbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.CreatedAt = DateTime.UtcNow;
            baseEntity.ModifiedAt = DateTime.UtcNow;
        }

        await this.DbContext.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public void AddRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
        }

        this.DbContext.AddRange(entities);
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
        }

        await this.DbContext.AddRangeAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public T? Get(Expression<Func<T, bool>> expression)
        => this.EntitySet.FirstOrDefault(expression);

    /// <inheritdoc/>
    public IEnumerable<T> GetAll()
        => this.EntitySet.AsEnumerable();

    /// <inheritdoc/>
    public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        => this.EntitySet.Where(expression).AsEnumerable();

    /// <inheritdoc/>
    public IEnumerable<T> GetAll(int skip, int take)
        => this.EntitySet.Skip(skip).Take(take).AsEnumerable();

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.EntitySet.ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await this.EntitySet.Where(expression).ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default)
        => await this.EntitySet.Skip(skip).Take(take).ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await this.EntitySet.FirstOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc/>
    public void Remove(T entity)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.IsDeleted = true;
            baseEntity.ModifiedAt = DateTime.UtcNow;
            this.DbContext.Update(entity); // Important: Update instead of Remove
        }
        else
        {
            this.DbContext.Remove(entity); // In rare case if entity is not BaseEntity
        }
    }

    /// <inheritdoc/>
    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
        }

        this.DbContext.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public void Update(T entity)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.ModifiedAt = DateTime.UtcNow;
        }

        this.DbContext.Update(entity);
    }

    /// <inheritdoc/>
    public void UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
        }

        this.DbContext.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public int Count()
        => this.EntitySet.Count();

    /// <inheritdoc/>
    public async Task<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await this.EntitySet.AnyAsync(expression, cancellationToken);

    /// <inheritdoc/>
    public IQueryable<T> GetAllAsQueryable()
        => this.EntitySet.AsQueryable();

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.DbContext.SaveChangesAsync(cancellationToken);
    }
}
