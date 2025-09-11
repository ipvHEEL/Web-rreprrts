using System;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Reports.Infrastructure.Data;

namespace Reports.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DwhDbContext _dwhDbContext;
    private readonly DbSet<TEntity> _dbSet;
    protected Repository(DwhDbContext dwhDbContext)
    {
        _dwhDbContext = dwhDbContext;
        _dbSet = _dwhDbContext.Set<TEntity>();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync();
        if(entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> GetValueAsync(int key)
    {
        return await _dbSet.FindAsync(key);
    }

    public virtual async Task SaveChangesAsync()
    {
        await _dwhDbContext.SaveChangesAsync();
    }
    public virtual void UpdateAsync(TEntity entity)
    {
        _dwhDbContext.Entry(entity).State = EntityState.Modified;
    }
}
