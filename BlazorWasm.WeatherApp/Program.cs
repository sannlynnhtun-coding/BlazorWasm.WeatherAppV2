using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorWasm.WeatherApp;
using BlazorWasm.WeatherApp.Pages;
using BlazorWasm.WeatherApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddMudServices();
builder.Services.AddGeolocationServices();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();
builder.Services.AddScoped<IFiveDaysForecastService, FiveDaysForecastService>();
builder.Services.AddTransient<ITodayHighlightsService, TodayHighlightsService>();
builder.Services.AddTransient<ITodayForecastService, TodayForecastService>();
builder.Services.AddTransient<IGeoCityService, GeoCityService>();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<IDbService, LocalStorageService>();

await builder.Build().RunAsync();