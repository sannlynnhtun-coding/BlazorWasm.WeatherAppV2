using System.Net.Http.Json;
using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services;

public class TodayHighlightsService : ITodayHighlightsService
{
    private readonly HttpClient _httpClient;

    public TodayHighlightsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TodayHighlights?> GetAsync(string appId, double lat, double lon)
    {
        string requestUrl =
            $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={appId}";
        var response = await _httpClient.GetFromJsonAsync<TodayHighlights>(requestUrl);
        return response;
    }
}