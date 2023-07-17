using Blazored.SessionStorage;
using BlazorWasm.WeatherApp;
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
builder.Services.AddScoped<ICurrentWeatherService>();
builder.Services.AddScoped<IFiveDaysForecastService>();

await builder.Build().RunAsync();
