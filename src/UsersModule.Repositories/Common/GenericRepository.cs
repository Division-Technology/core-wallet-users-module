using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UsersModule.Data;

namespace UsersModule.Repositories.Common;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly UsersModuleDbContext DbContext;
    protected readonly DbSet<T> EntitySet;

    public GenericRepository(UsersModuleDbContext dbContext)
    {
        this.DbContext = dbContext;
        EntitySet = DbContext.Set<T>();
    }
    
    public void Add(T entity)
        => DbContext.Add(entity);

    public async Task AddAndSaveAsync(T entity)
    {
        Add(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await DbContext.AddAsync(entity, cancellationToken);

    public void AddRange(IEnumerable<T> entities)
        => DbContext.AddRange(entities);

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        => await DbContext.AddRangeAsync(entities, cancellationToken);

    public T? Get(Expression<Func<T, bool>> expression)
        => EntitySet.FirstOrDefault(expression);

    public IEnumerable<T> GetAll()
        => EntitySet.AsEnumerable();

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        => EntitySet.Where(expression).AsEnumerable();

    public IEnumerable<T> GetAll(int skip, int take)
        => EntitySet.Skip(skip).Take(take).AsEnumerable();

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await EntitySet.ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await EntitySet.Where(expression).ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default)
        => await EntitySet.Skip(skip).Take(take).ToListAsync(cancellationToken);

    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await EntitySet.FirstOrDefaultAsync(expression, cancellationToken);

    public void Remove(T entity)
        => DbContext.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
        => DbContext.RemoveRange(entities);

    public void Update(T entity)
        => DbContext.Update(entity);

    public void UpdateRange(IEnumerable<T> entities)
        => DbContext.UpdateRange(entities);

    public int Count()
        => EntitySet.Count();

    public async Task<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        => await EntitySet.AnyAsync(expression, cancellationToken);

    public IQueryable<T> GetAllAsQueryable() => EntitySet.AsQueryable();
}