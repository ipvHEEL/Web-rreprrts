using System;

using Reports.Domain.ProductionOrder;
using Reports.Infrastructure.Repositories;
using Reports.Api.Extension;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Reports.Api.Services;

public class ProductionOrderService : IProductionOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductionOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductionOrderNumberDto>> GetByStartDateAsync(DateOnly startDate)
    {
        int dateInt = startDate.ConvertToInt();
        var repo = await _unitOfWork.ProductionOrderRepository.GetByStartDateAsync(dateInt);
        return await repo.Select(po => new ProductionOrderNumberDto
        {
            ProductionOrder = po.Ka0133ProdAuftrNr
        }).ToListAsync();
    }

    public Task<IEnumerable<ProductionOrderDto>> GetByStartDateAsync(DateOnly startdDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductionOrderDto>> GetByStartDateAsync(List<DateOnly> dates)
    {
        throw new NotImplementedException();
    }
}
