using System;
using Reports.Infrastructure.Data;

namespace Reports.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DwhDbContext _dwhDbContext;
    private bool disposed = false;

    public IProductionOrderRepository ProductionOrderRepository { get; }

    public UnitOfWork(DwhDbContext dwhDbContext)
    {
        _dwhDbContext = dwhDbContext ?? throw new ArgumentNullException(nameof(dwhDbContext));
        ProductionOrderRepository = new ProductionOrderRepository(_dwhDbContext);
    }

    public async Task<int> CompleteAsync()
    {
        return await _dwhDbContext.SaveChangesAsync(); // Сохраняем изменения контекста
    }
    #region IDisposable implementation

    protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dwhDbContext.Dispose();
                }
                disposed = true;
            }
        }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
