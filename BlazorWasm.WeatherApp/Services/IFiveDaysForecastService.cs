using System.Net.Http.Json;
using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services;

public class IFiveDaysForecastService
{
    private readonly HttpClient _httpClient;

    public IFiveDaysForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FiveDayForecast?> GetAsync(string appId, double lat, double lon)
    {
        string requestUrl =
            $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={appId}";
        var response = await _httpClient.GetFromJsonAsync<FiveDayForecast>(requestUrl);
        return response;
    }
}