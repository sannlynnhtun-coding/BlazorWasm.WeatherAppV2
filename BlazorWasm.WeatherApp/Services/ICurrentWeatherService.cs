using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services;

public interface ICurrentWeatherService
{
    Task<CurrentWeather?> GetAsync(string appId, double lat, double lon);
}