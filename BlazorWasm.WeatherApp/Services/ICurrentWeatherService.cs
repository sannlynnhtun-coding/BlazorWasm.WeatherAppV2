using System.Net.Http.Json;
using BlazorWasm.WeatherApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorWasm.WeatherApp.Services;

public class ICurrentWeatherService
{
    private readonly HttpClient _httpClient;

    public ICurrentWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CurrentWeather?> GetAsync(string appId, double lat, double lon)
    {
        string requestUrl =
            $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={appId}";
        var response = await _httpClient.GetFromJsonAsync<CurrentWeather>(requestUrl);
        return response;
    }
}