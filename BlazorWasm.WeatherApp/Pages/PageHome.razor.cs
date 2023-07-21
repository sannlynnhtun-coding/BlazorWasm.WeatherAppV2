using BlazorWasm.WeatherApp.Models;
using MudBlazor;

namespace BlazorWasm.WeatherApp.Pages;

using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static MudBlazor.CategoryTypes;

public partial class PageHome
{
    private double _latitude;
    private double _longitude;
    private bool _toggleSearch;
    private CurrentWeather? _currentWeather;
    private FiveDayForecast? _fiveDayForecast;
    private TodayHightlights? _todayHightlights;
    private TodayForecast? _todayForecast;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            GeolocationService.GetCurrentPosition(this,
                nameof(OnCoordinatesPermitted),
                nameof(OnErrorRequestingCoordinates));

            var GetApiKey = await db.GetApiKey();
            IEnumerable<string> EnumAppId = GetApiKey.Select(x => x.AppId);
            string AppId = string.Join("", EnumAppId);

            if (GetApiKey == null || GetApiKey.Count == 0)
            {
                await DialogBox();
            }
            else
            {
                await Task.Delay(3000);
                await WeatherTasks(AppId);
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

    async Task ShowApi()
    {
        await DialogBox();
    }
    public async Task WeatherTasks(string AppId)
    {
        var taskCurrentWeather =
                            CurrentWeatherService.GetAsync(AppId, _latitude, _longitude);
        var taskFiveDaysForecast =
            FiveDaysForecastService.GetAsync(AppId, _latitude, _longitude);
        var takTodayHighlights =
            TodayHightlightsService.GetAsync(AppId, _latitude, _longitude);
        var takTodayForecast =
            TodayForecastService.GetAsync(AppId, _latitude, _longitude);
        await Task.WhenAll(taskCurrentWeather, taskFiveDaysForecast,
            takTodayHighlights, takTodayForecast);

        _currentWeather = taskCurrentWeather.Result;
        _fiveDayForecast = taskFiveDaysForecast.Result;
        _todayHightlights = takTodayHighlights.Result;
        _todayForecast = takTodayForecast.Result;

        StateHasChanged();
    }

    public async Task DialogBox()
    {
        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
        var dialog = await DialogService.ShowAsync<PageApiKey>("Input Open Weather Api Key", maxWidth);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var apiKeyModel = result.Data as ApiKeyModel;
            if (apiKeyModel is { AppId: not null } && !string.IsNullOrEmpty(apiKeyModel.AppId))
            {
                await db.SetApiKey(apiKeyModel);
                await WeatherTasks(apiKeyModel.AppId);
            }
        }
    }
}