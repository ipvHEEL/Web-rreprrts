using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Planning.API.Model;

namespace Planning.API.Apis;

public static class ItemStockApi
{
    public static IEndpointRouteBuilder MapItemStockApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getitemstock", GetAllItemStock);
        app.MapGet("/getitem", GetItem);
        return app;
    }

    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Ok<PaginatedItems<ItemData>>> GetItem(
        [AsParameters] PaginationRequest paginationRequest,
        DT0012MainRequest.DT0012MAINRequestServiceSoap serviceSoap)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;
        var itemStockResponse = await serviceSoap.SOADT0012MAINRequestAsync(
            new DT0012MainRequest.SOADT0012MAINRequestRequest(
                new DT0012MainRequest.DT0012MAINRequest()
                {
                    T2f1 = 1010017483,
                    T2f1Specified = true
                }));
        var totalItems = itemStockResponse.SOADT0012MAINResult.LongCount();
        var itemsOnPage = itemStockResponse.SOADT0012MAINResult
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(response => new ItemData
            {
                Item = response.T2f1,
                Description1 = response.T18f1,
                HostItem = response.T24f1
            })
            .ToList();
        return TypedResults.Ok(new PaginatedItems<ItemData>(pageIndex, pageSize, totalItems, itemsOnPage));
    }

    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Ok<PaginatedItems<ItemStock>>> GetAllItemStock(
        [AsParameters] PaginationRequest paginationRequest,
        DT0051GETBESTANDRequestServiceSoap serviceSoap)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;
        var itemStockResponse = await serviceSoap.SOADT0051GETBESTANDRequestAsync(
            new SOADT0051GETBESTANDRequestRequest(
                new DT0051GETBESTANDRequest()
                {
                    L1f1 = 1,
                    L1f1Specified = true,
                    L1f3 = 3012,
                    L1f3Specified = true
                }));
        var totalItems = itemStockResponse.SOADT0051GETBESTANDResult.LongCount();
        var itemsOnPage = itemStockResponse.SOADT0051GETBESTANDResult
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(response => new ItemStock
            {
                Item = response.L1f2,
                Description = response.L4f2,
                BalanceQuantity = response.L1f14,
                UnitOfMeasure = response.L1f9
            })
            .ToList();
        return TypedResults.Ok(new PaginatedItems<ItemStock>(pageIndex, pageSize, totalItems, itemsOnPage));
    }
}
