using BlazorWasm.WeatherApp.Models;
using System.Net.Http.Json;

namespace BlazorWasm.WeatherApp.Services
{
    public class GeoCityService : IGeoCityService
    {
        private readonly HttpClient _httpClient;
        public GeoCityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CityInfo[]?> GetAsync(string appId, string name)
        {
            string requestUrl =
                $"https://api.openweathermap.org/geo/1.0/direct?q={name}&limit=5&appid={appId}";
            var response = await _httpClient.GetFromJsonAsync<CityInfo[]>(requestUrl);
            return response;
        }
    }
}
