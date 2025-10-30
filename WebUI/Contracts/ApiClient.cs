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
            using var response = await _httpClient.GetAsync(apiController);
            _logger.LogInformation(response.EnsureSuccessStatusCode().ToString());

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T?> GetObjectByIdAsync<T>(string apiController)
        {
            using var response = await _httpClient.GetAsync(apiController);
            _logger.LogInformation(response.EnsureSuccessStatusCode().ToString());

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SendObject<T>(string apiController, T obj) 
        {
            using var response = await _httpClient.PostAsJsonAsync(apiController, obj);
            _logger.LogInformation($"Отправка объекта - Статус от API: {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<T?> PostAndReadAsync<T>(string apiController, object obj)
        {
            using var response = await _httpClient.PostAsJsonAsync(apiController, obj);
            _logger.LogInformation($"Статус от API: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
                return default;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task DeleteObject(string apiController) 
        {
            using var response = await _httpClient.DeleteAsync(apiController);
            _logger.LogInformation($"Отправленный Json в {apiController}");

            response.EnsureSuccessStatusCode();
        }

        public async Task PatchObjectById(string apiController)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{apiController}");

            using var response = await _httpClient.SendAsync(request);

            _logger.LogInformation($"PATCH {apiController}, статус: {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }

    }
}
