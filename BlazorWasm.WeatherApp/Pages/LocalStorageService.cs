using Blazored.LocalStorage;
using BlazorWasm.WeatherApp.Models;
using BlazorWasm.WeatherApp.Services;

namespace BlazorWasm.WeatherApp.Pages
{
    public class LocalStorageService : IDbService
    {
        private readonly ILocalStorageService _service;
        public LocalStorageService(ILocalStorageService service)
        {
            _service = service;
        }

        public async Task<ApiKeyModel?> GetApiKey()
        {
            var apiKey = await _service.GetItemAsync<ApiKeyModel?>("ApiKey");
            return apiKey;
        }
        public async Task SetApiKey(ApiKeyModel apiKey)
        {
            await _service.SetItemAsync("ApiKey", apiKey);
        }
    }
}
