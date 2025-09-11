using System;
using System.Linq.Expressions;

namespace Reports.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetValueAsync(int key);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
