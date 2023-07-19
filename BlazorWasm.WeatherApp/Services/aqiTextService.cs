using BlazorWasm.WeatherApp.Models;

namespace BlazorWasm.WeatherApp.Services
{
    public static class aqiTextService
    {
        public static List<AqiTextModel> GetAqiTextList => 
            AqiTextList<AqiTextModel>(JsonData.AqiText).ToList();

        public static AqiTextModel GetAqiText(string id)
        {
            var item = GetAqiTextList.Where(x=> x.id == id).FirstOrDefault();
            return item;
        }

        public static List<T> AqiTextList<T>(string json)
        {
            var list = json.ToObject<List<T>>();
            return list;
        }
    }

    public static class JsonData
    {
        public static string AqiText { get; } = @"[
   {
""id"": ""1"",
    ""level"": ""Good"",
    ""message"": ""Air quality is considered satisfactory, and air pollution poses little or no risk""
  },
  {
""id"": ""2"",
    ""level"": ""Fair"",
    ""message"": ""Air quality is acceptable; however, for some pollutants there may be a moderate health concern for a very small number of people who are unusually sensitive to air pollution.""
  },
  {
""id"": ""3"",
    ""level"": ""Moderate"",
    ""message"": ""Members of sensitive groups may experience health effects. The general public is not likely to be affected.""
  },
   {
 ""id"": ""4"",
    ""level"": ""Poor"",
    ""message"": ""Everyone may begin to experience health effects; members of sensitive groups may experience more serious health effects""
  },
 {
""id"": ""5"",
    ""level"": ""Very Poor"",
    ""message"": ""Health warnings of emergency conditions. The entire population is more likely to be affected.""
  }
]";
    }
}
