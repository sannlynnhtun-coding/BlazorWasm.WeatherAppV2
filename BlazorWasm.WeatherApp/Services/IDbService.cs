using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services
{
    public interface IDbService
    {
        Task<ApiKeyModel?> GetApiKey();
        Task SetApiKey(ApiKeyModel api);
    }
}
