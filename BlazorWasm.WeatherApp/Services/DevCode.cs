using System.Globalization;
using Newtonsoft.Json;

namespace BlazorWasm.WeatherApp.Services;

public static class DevCode
{
    public static DateTime GetUtcDateTime(this int dt, int timezone)
    {
        long dtStr = Convert.ToInt64(dt);
        long timezoneStr = Convert.ToInt64(timezone);
        DateTime dateTime = new DateTime((dtStr + timezoneStr) * 1000);

        // long dtStr = Convert.ToInt64(dt);
        // long timezoneStr = Convert.ToInt64(timezone);
        // DateTime dateTime = new DateTime(dtStr * timezoneStr);
        return dateTime;
    }

    public static DateTime ToDateTimeForWeather(this string str)
    {
        string format = "yyyy-MM-dd HH:mm:ss";
        DateTime.TryParseExact(
            str,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, 
            out var resultDateTime);
        return resultDateTime;
    }

    public static T ToObject<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}