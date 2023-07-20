using BlazorWasm.WeatherApp.Models;
using System.Net.Http.Json;

namespace BlazorWasm.WeatherApp.Services
{
    public interface ITodayForecastService
    {
        Task<TodayForecast?> GetAsync(string appId, double lat, double lon);
    }
}
