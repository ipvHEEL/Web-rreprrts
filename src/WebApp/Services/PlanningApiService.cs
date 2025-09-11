using Microsoft.AspNetCore.Mvc.RazorPages;

using static System.Net.WebRequestMethods;
using static WebApp.Components.Pages.Planning;

namespace WebApp.Services;

public class PlanningApiService
{
    private readonly HttpClient _httpClient;

    public PlanningApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse> GetResponse(int pageIndex, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse>($"getitemstock?pageIndex={pageIndex}&pageSize={pageSize}");
    }
}