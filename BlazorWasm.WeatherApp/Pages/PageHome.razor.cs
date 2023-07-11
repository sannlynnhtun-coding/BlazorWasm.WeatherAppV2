using BlazorWasm.WeatherApp.Models;
using MudBlazor;

namespace BlazorWasm.WeatherApp.Pages;

using Microsoft.JSInterop;

public partial class PageHome
{
    private double _latitude;
    private double _longitude;
    private bool toggleSearch = false;
    private CurrentWeather _currentWeather;

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
                if (apiKeyModel.AppId != null && !string.IsNullOrEmpty(apiKeyModel.AppId))
                    _currentWeather = await CurrentWeatherService.GetAsync(apiKeyModel.AppId, _latitude, _longitude);
                StateHasChanged();
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
        toggleSearch = !toggleSearch;
    }
}