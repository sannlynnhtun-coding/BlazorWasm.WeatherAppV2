using BlazorWasm.WeatherApp.Models;
using BlazorWasm.WeatherApp.Services;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace BlazorWasm.WeatherApp.Pages;

using Microsoft.JSInterop;

public partial class PageHome
{
    private string? _search;
    private bool _isSearching = false;
    private double _latitude = 21.9588;
    private double _longitude = 96.0891;
    private bool _toggleSearch;
    private CurrentWeather? _currentWeather;
    private FiveDayForecast? _fiveDayForecast;
    private TodayHighlights? _todayHighlights;
    private TodayForecast? _todayForecast;
    private ApiKeyModel? _apiKey;
    public bool? IsCurrentLocation;
    private CityInfo[]? _cityInfos;
    private string CurrentLocationButtonDisabled => (IsCurrentLocation ?? false) ? "disabled" : "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // SetCurrentLocation();

            _apiKey = await Db.GetApiKey();
            if (_apiKey is null)
            {
                await OpenDialogBox();
            }
            else
            {
                await Task.Delay(3000);
                await WeatherTasks();
            }
        }
    }

    private async Task SetCurrentLocation()
    {
        GeolocationService.GetCurrentPosition(this,
            nameof(OnCoordinatesPermitted),
            nameof(OnErrorRequestingCoordinates));
        await Task.Delay(3000);
        await WeatherTasks();
    }

    [JSInvokable]
    public async Task OnCoordinatesPermitted(
        GeolocationPosition position)
    {
        try
        {
            _latitude = position.Coords.Latitude;
            _longitude = position.Coords.Longitude;
            IsCurrentLocation = true;
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [JSInvokable]
    public async Task OnErrorRequestingCoordinates(
        GeolocationPositionError error)
    {
        try
        {
            _latitude = 21.9588;
            _longitude = 96.0891;
            IsCurrentLocation = false;

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ToggleSearch()
    {
        _toggleSearch = !_toggleSearch;
    }

    private async Task ShowApi() => await OpenDialogBox();

    private async Task WeatherTasks()
    {
        if (_apiKey != null)
        {
            string appId = _apiKey.AppId;

            var taskCurrentWeather =
                CurrentWeatherService.GetAsync(appId, _latitude, _longitude);
            var taskFiveDaysForecast =
                FiveDaysForecastService.GetAsync(appId, _latitude, _longitude);
            var taskTodayHighlights =
                TodayHighlightsService.GetAsync(appId, _latitude, _longitude);
            var taskTodayForecast =
                TodayForecastService.GetAsync(appId, _latitude, _longitude);
            await Task.WhenAll(
                taskCurrentWeather,
                taskFiveDaysForecast,
                taskTodayHighlights,
                taskTodayForecast);

            _currentWeather = taskCurrentWeather.Result;
            _fiveDayForecast = taskFiveDaysForecast.Result;
            _todayHighlights = taskTodayHighlights.Result;
            _todayForecast = taskTodayForecast.Result;
        }

        // StateHasChanged();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OpenDialogBox()
    {
        var maxWidth = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
        var dialog = await DialogService.ShowAsync<PageApiKey>(
            "Input Open Weather API Key", maxWidth);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            if (result.Data is ApiKeyModel { AppId: not null } apiKeyModel
                && !string.IsNullOrEmpty(apiKeyModel.AppId))
            {
                await Db.SetApiKey(apiKeyModel);
                _apiKey = apiKeyModel;
                await WeatherTasks();
            }
        }
    }

    private async Task HandelKeyPress(KeyboardEventArgs e)
    {
        if (e.Key is not "Enter")
            return;

        if (_search == null || _apiKey == null) return;
        _isSearching = true;
        _cityInfos = await GeoCityService.GetAsync(_apiKey.AppId, _search);
        _isSearching = false;
        StateHasChanged();
    }

    private async Task FindLocation(CityInfo cityInfo)
    {
        _latitude = cityInfo.lat;
        _longitude = cityInfo.lon;
        IsCurrentLocation = false;
        await WeatherTasks();
    }
}