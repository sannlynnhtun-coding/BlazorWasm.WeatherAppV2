using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services
{
    public interface ITodayHighlightsService
    {
        Task<TodayHightlights?> GetAsync(string appId, double lat, double lon);
    }
}
