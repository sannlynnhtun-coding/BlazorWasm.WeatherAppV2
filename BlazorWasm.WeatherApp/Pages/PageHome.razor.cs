using BlazorWasm.WeatherApp.Models;
using MudBlazor;

namespace BlazorWasm.WeatherApp.Pages;

using Microsoft.JSInterop;

public partial class PageHome
{
    private double _latitude;
    private double _longitude;
    private bool _toggleSearch;
    private CurrentWeather? _currentWeather;
    private FiveDayForecast? _fiveDayForecast;
    private TodayHightlights? _todayHightlights;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            GeolocationService.GetCurrentPosition(this,
                nameof(OnCoordinatesPermitted),
                nameof(OnErrorRequestingCoordinates));
            DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
            var dialog = await DialogService.ShowAsync<PageApiKey>("Input Open Weather Api Key", maxWidth);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var apiKeyModel = result.Data as ApiKeyModel;
                if (apiKeyModel is { AppId: not null } && !string.IsNullOrEmpty(apiKeyModel.AppId))
                {
                    var taskCurrentWeather = 
                        CurrentWeatherService.GetAsync(apiKeyModel.AppId, _latitude, _longitude);
                    var taskFiveDaysForecast =
                        FiveDaysForecastService.GetAsync(apiKeyModel.AppId, _latitude, _longitude);
                    var takTodayHightligths =
                        TodayHightlightsService.GetAsync(apiKeyModel.AppId, _latitude, _longitude);
                    await Task.WhenAll(taskCurrentWeather, taskFiveDaysForecast,
                        takTodayHightligths);

                    _currentWeather = taskCurrentWeather.Result;
                    _fiveDayForecast = taskFiveDaysForecast.Result;
                    _todayHightlights = takTodayHightligths.Result;

                    StateHasChanged();
                }
            }
        }
    }

    [JSInvokable]
    public async Task OnCoordinatesPermitted(
        GeolocationPosition position)
    {
        // TODO: consume/handle position.
        _latitude = position.Coords.Latitude;
        _longitude = position.Coords.Longitude;
        await InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public async Task OnErrorRequestingCoordinates(
        GeolocationPositionError error)
    {
        // TODO: consume/handle error.

        await InvokeAsync(StateHasChanged);
    }

    void ToggleSearch()
    {
        _toggleSearch = !_toggleSearch;
    }
}