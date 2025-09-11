using System;

using Reports.Infrastructure.Entities;

namespace Reports.Infrastructure.Repositories;

public interface IProductionOrderRepository : IRepository<CpDwhKa0133>
{
    Task<IQueryable<CpDwhKa0133>> GetByStartDateAsync(int startDate);
    Task<IEnumerable<CpDwhKa0133>> GetByStartDateAsync(int startDate, int endDate);
    Task<IEnumerable<CpDwhKa0133>> GetByStartDateAsync(List<int> dates);
}
