using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services;

public interface IGeoCityService
{
    Task<CityInfo[]?> GetAsync(string appId, string name);
}