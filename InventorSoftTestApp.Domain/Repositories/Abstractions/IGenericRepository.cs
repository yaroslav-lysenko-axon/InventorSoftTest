using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventorSoftTestApp.Domain.Repositories.Abstractions;

public interface IGenericRepository<T>
    where T : class
{
    Task<List<T>> FindAll(params Expression<Func<T, object>>[] includes);
    
    Task<List<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    ValueTask<EntityEntry<T>> InsertAsync(T entity);

    void UpdateRange(IEnumerable<T> entity);
}