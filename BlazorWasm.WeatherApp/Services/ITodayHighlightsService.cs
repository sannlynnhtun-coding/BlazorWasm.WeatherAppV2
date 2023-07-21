using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services
{
    public interface ITodayHighlightsService
    {
        Task<TodayHighlights?> GetAsync(string appId, double lat, double lon);
    }
}
