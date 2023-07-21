using Newtonsoft.Json;

namespace BlazorWasm.WeatherApp.Models;

public class ApiKeyModel
{
    public string AppId { get; set; }
}

public class CurrentWeather
{
    public Coord coord { get; set; }
    public Weather[] weather { get; set; }
    // public string base { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Rain rain { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public int deg { get; set; }
}

public class Rain
{
    public double _h { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class FiveDayForecast
{
    public string cod { get; set; }
    public int message { get; set; }
    public int cnt { get; set; }
    public DayForecastList[] list { get; set; }
    public City city { get; set; }
}

public class DayForecastList
{
    public int dt { get; set; }
    public Main main { get; set; }
    public Weather[] weather { get; set; }
    public Clouds clouds { get; set; }
    public Wind wind { get; set; }
    public int visibility { get; set; }
    public double pop { get; set; }
    public Sys sys { get; set; }
    public string dt_txt { get; set; }
    public Rain rain { get; set; }
}

public class City
{
    public int id { get; set; }
    public string name { get; set; }
    public Coord coord { get; set; }
    public string country { get; set; }
    public int population { get; set; }
    public int timezone { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class TodayHighlights
{
    public Coord coord { get; set; }
    public List<List> list { get; set; }
}

public class List
{
    public MainAqi main { get; set; }
    public Components components { get; set; }
    public int dt { get; set; }
}
public class Components
{
    public double co { get; set; }
    public double no { get; set; }
    public double no2 { get; set; }
    public double o3 { get; set; }
    public double so2 { get; set; }
    public double pm2_5 { get; set; }
    public double pm10 { get; set; }
    public double nh3 { get; set; }
}
public class MainAqi
{
    public int aqi { get; set; }
}

public class AqiTextModel
{
    public string id { get; set; }
    public string level { get; set; }
    public string message { get; set; }
}

public class ListToday
{
    public int dt { get; set; }
    public Main main { get; set; }
    public List<Weather> weather { get; set; }
    public Clouds clouds { get; set; }
    public Wind wind { get; set; }
    public int visibility { get; set; }
    public double pop { get; set; }
    public SysToday sys { get; set; }
    public string dt_txt { get; set; }
    public Rain rain { get; set; }
}

public class MainToday : Main
{
    public int sea_level { get; set; }
    public int grnd_level { get; set; }
    public double temp_kf { get; set; }
}

public class TodayForecast
{
    public string cod { get; set; }
    public int message { get; set; }
    public int cnt { get; set; }
    public List<ListToday> list { get; set; }
    public City city { get; set; }
}

public class SysToday
{
    public string pod { get; set; }
}

public class WindToday : Wind
{
    public double gust { get; set; }
}

public class CityInfo
{
    public string name { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public string country { get; set; }
    public string state { get; set; }
}

