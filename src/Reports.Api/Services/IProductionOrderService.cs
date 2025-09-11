using System;

using Reports.Domain.ProductionOrder;

namespace Reports.Api.Services;

public interface IProductionOrderService
{
    Task<IEnumerable<ProductionOrderNumberDto>> GetByStartDateAsync(DateOnly startDate);
    Task<IEnumerable<ProductionOrderDto>> GetByStartDateAsync(DateOnly startdDate, DateOnly endDate);
    Task<IEnumerable<ProductionOrderDto>> GetByStartDateAsync(List<DateOnly> dates);
}
