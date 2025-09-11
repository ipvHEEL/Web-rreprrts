using System;

namespace Reports.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    IProductionOrderRepository ProductionOrderRepository { get; }
    Task<int> CompleteAsync();
}
