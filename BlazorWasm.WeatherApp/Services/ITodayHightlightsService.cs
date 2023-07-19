using BlazorWasm.WeatherApp.Models;
using System.Net.Http.Json;

namespace BlazorWasm.WeatherApp.Services
{
    public class ITodayHightlightsService
    {
        private readonly HttpClient _httpClient;

        public ITodayHightlightsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TodayHightlights?> GetAsync(string appId, double lat, double lon)
        {
            string requestUrl =
                $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={appId}";
            var response = await _httpClient.GetFromJsonAsync<TodayHightlights>(requestUrl);
            return response;
        }
    }
}
