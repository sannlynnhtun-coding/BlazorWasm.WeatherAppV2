namespace BlazorWasm.WeatherApp.Services;

public static class DevCode
{
    public static DateTime GetUtcDateTime(this int dt, int timezone)
    {
        long dtStr = Convert.ToInt64(dt);
        long timezoneStr = Convert.ToInt64(timezone);
        DateTime dateTime = new DateTime((dtStr + timezoneStr) * 1000);
        return dateTime;
    }
}