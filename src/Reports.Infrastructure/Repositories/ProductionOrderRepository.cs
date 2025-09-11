using System;
using System.Runtime.InteropServices;
using Reports;

using Microsoft.EntityFrameworkCore;

using Reports.Infrastructure.Data;
using Reports.Infrastructure.Entities;
using Reports.Domain.ProductionOrder;
using Azure;

namespace Reports.Infrastructure.Repositories;

public class ProductionOrderRepository : Repository<CpDwhKa0133>, IProductionOrderRepository
{
    public ProductionOrderRepository(DwhDbContext dbContext) : base(dbContext) {}

    public async Task<IQueryable<CpDwhKa0133>> GetByStartDateAsync(int startDate)
    {
        return _dwhDbContext.CpDwhKa0133s.Where(sd => sd.Ka0133StartDatumSoll == startDate).AsQueryable();
    }
    public async Task<IEnumerable<CpDwhKa0133>> GetByStartDateAsync(int startDate, int endDate)
    {
        return await _dwhDbContext.CpDwhKa0133s.Where(sd => sd.Ka0133StartDatumSoll >= startDate & sd.Ka0133StartDatumSoll <= endDate).ToListAsync();
    }
    public async Task<IEnumerable<CpDwhKa0133>> GetByStartDateAsync(List<int> dates)
    {
        return await _dwhDbContext.CpDwhKa0133s.Where(sd => dates.Contains(sd.Ka0133StartDatumSoll)).ToListAsync();
    }
    public async Task<(IEnumerable<ProductionOrderNumberDto> Items, int Total)> GetPagedAsync(IQueryable<CpDwhKa0133> query, int page, int pageSize)
    {
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(po => new ProductionOrderNumberDto
            {
                ProductionOrder = po.Ka0133ProdAuftrNr
            }).ToListAsync();
        return (items, total);
    }
}
