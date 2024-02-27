using System.Linq.Expressions;
using InventorSoftTestApp.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventorSoftTestApp.Domain.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;

    protected GenericRepository(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
    }   
    
    public Task<List<T>> FindAll(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query.AsNoTracking().ToListAsync();
    }
    
    public Task<List<T>> Find(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.Where(predicate);

        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.ToListAsync();
    }
    
    public ValueTask<EntityEntry<T>> InsertAsync(T entity)
    {
        return _dbSet.AddAsync(entity);
    }
    
    public void UpdateRange(IEnumerable<T> entity)
    {
        _dbSet.UpdateRange(entity);
    }
}