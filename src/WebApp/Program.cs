using System.Globalization;

using WebApp;
using WebApp.Components;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Настройка культуры
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
var supportedCultures = new[]
{
    new CultureInfo("ru-RU"),
    new CultureInfo("en-GB")
};
var localizationIptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ReportsApiService>(client =>
{
    var config = builder.Configuration.GetSection("ApiEndpoints").Get<ApiConfig>();
    client.BaseAddress = new Uri(config.ReportsApi);
}).SetHandlerLifetime(TimeSpan.FromMinutes(30));

builder.Services.AddHttpClient<PlanningApiService>(client =>
{
    var config = builder.Configuration.GetSection("ApiEndpoints").Get<ApiConfig>();
    client.BaseAddress = new Uri(config.PlanningApi);
}).SetHandlerLifetime(TimeSpan.FromMinutes(30));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRequestLocalization(localizationIptions);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
