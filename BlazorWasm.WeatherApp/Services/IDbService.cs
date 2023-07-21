using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services
{
    public interface IDbService
    {
        Task<List<ApiKeyModel>> GetApiKey();
        Task SetApiKey(ApiKeyModel api);
    }
}
