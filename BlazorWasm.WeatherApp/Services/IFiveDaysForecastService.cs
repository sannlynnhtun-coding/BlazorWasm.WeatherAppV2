using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services;

public interface IFiveDaysForecastService
{
    Task<FiveDayForecast?> GetAsync(string appId, double lat, double lon);
}