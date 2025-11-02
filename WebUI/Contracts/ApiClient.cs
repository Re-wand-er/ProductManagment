using ProductManagment.WebUI.Contracts.ErrorModels;
using System;
using System.Text;
using System.Text.Json;

namespace ProductManagment.WebUI.Contracts
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(IHttpClientFactory factory, ILogger<ApiClient> logger) 
        { 
            _httpClient = factory.CreateClient("ApiClient");
            _logger = logger;
        }

        public async Task<IEnumerable<T>?> GetObjectListAsync<T>(string apiController)
        {
            _logger.LogInformation($"Статус от API при получении всех объектов по пути {apiController}");
            return await GetObjectAndResultAsync<IEnumerable<T>>(apiController);
        }

        public async Task<T?> GetObjectByIdAsync<T>(string apiController)
        {
            _logger.LogInformation($"Статус от API при получении одного объекта по пути {apiController}");
            return await GetObjectAndResultAsync<T>(apiController);
        }

        public async Task<IEnumerable<T>?> GetFilterObjectAsync<T>(string url)
        { 
            _logger.LogInformation($"Статус от API при фильтрации всех объектов по пути {url}");
            return await GetObjectAndResultAsync<IEnumerable<T>>(url);
        }

        public async Task<string?> SendObject<T>(string apiController, T obj) 
        {
            using var response = await _httpClient.PostAsJsonAsync(apiController, obj);
            _logger.LogInformation($"Статус отправленного Post-запроса от API: {response.StatusCode}");

            if(!response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<ApiResultResponse<T>> PostAndReadAsync<T>(string apiController, object obj)
        {
            using var response = await _httpClient.PostAsJsonAsync(apiController, obj);
            _logger.LogInformation($"Статус от API при Post-запросе по пути {apiController}:{response.StatusCode}");

            var apiResponse = new ApiResultResponse<T>();

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                apiResponse.ErrorMessage = error;

                _logger.LogWarning($"Ошибка API: {error}");
                return apiResponse; // null для ссылочных типов
            }

            apiResponse.Value = await response.Content.ReadFromJsonAsync<T>();
            return apiResponse;
        }

        public async Task DeleteObject(string apiController) 
        {
            using var response = await _httpClient.DeleteAsync(apiController);
            _logger.LogInformation($"Статус от APi при Delete-запросе по пути {apiController}: {response.StatusCode}");
            //response.EnsureSuccessStatusCode();
        }

        public async Task PatchObjectById(string apiController)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{apiController}");
            using var response = await _httpClient.SendAsync(request);

            _logger.LogInformation($"Статус от APi при Patch-запросе по пути {apiController}: {response.StatusCode}");
            //response.EnsureSuccessStatusCode();
        }

        // Private 
        //------------------------------------------------------------------------------------------------------------
        private async Task<T?> GetObjectAndResultAsync<T>(string apiController) 
        {
            using var response = await _httpClient.GetAsync(apiController);
            var json = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Статус от API: {response.StatusCode}");

            //response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
