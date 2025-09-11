using System;

using Microsoft.Extensions.Options;

using Reports.Domain.ProductionOrder;
namespace WebApp.Services;

public class ReportsApiService
{
    private readonly HttpClient _httpClient;
    public ReportsApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    internal async Task<List<ProductionOrderNumberDto>> GetOrderNumber(DateOnly dateOnly)
    {
        var dateString = dateOnly.ToString("yyyy-MM-dd");
        var response = await _httpClient.GetFromJsonAsync<List<ProductionOrderNumberDto>>(
            $"production-order/by-date?date={dateString}");
        return response;
    }
}
