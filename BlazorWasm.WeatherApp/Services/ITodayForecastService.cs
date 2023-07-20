using BlazorWasm.WeatherApp.Models;
using System.Net.Http.Json;

namespace BlazorWasm.WeatherApp.Services
{
    public class ITodayForecastService
    {
        private readonly HttpClient _httpClient;
        public ITodayForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }

        public async Task<TodayForecast?> GetAsync(string appId, double lat, double lon)
        {
            string requestUrl =
                $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={appId}";
            var response = await _httpClient.GetFromJsonAsync<TodayForecast>(requestUrl);
            return response;
        }
    }
}
