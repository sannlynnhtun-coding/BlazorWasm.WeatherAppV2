using Blazored.LocalStorage;
using BlazorWasm.WeatherApp.Models;
using BlazorWasm.WeatherApp.Services;

namespace BlazorWasm.WeatherApp.Pages
{
    public class LocalStorageService : IDbService
    {
        private readonly ILocalStorageService _service;
        public LocalStorageService(ILocalStorageService service)
        {
            _service = service;
        }

        public async Task<List<ApiKeyModel>> GetApiKey()
        {
            var GetKey = await _service.GetItemAsync<List<ApiKeyModel>>("ApiKey");
            GetKey ??= new();
            return GetKey;
        }
        public async Task SetApiKey(ApiKeyModel api)
        {
            var GetKey = await _service.GetItemAsync<List<ApiKeyModel>>("ApiKey");
            GetKey ??= new();
            IEnumerable<string> EnumAppId = GetKey.Select(x => x.AppId);
            string AppId = string.Join("", EnumAppId);
            if (GetKey == null || GetKey.Count == 0) 
            {
                GetKey.Add(api);            
            }
            else
            {
                var result =  GetKey.FirstOrDefault(x=> x.AppId == AppId);
                int index = GetKey.FindIndex(x=> x.AppId == result.AppId);
                GetKey[index] = api;
            }

            await _service.SetItemAsync("ApiKey", GetKey);
        }
    }
}
