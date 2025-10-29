namespace ProductManagment.Infrastructure.API
{
    public class ApiServer
    {
        private readonly HttpClient _httpClient;

        public ApiServer(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        //private readonly HttpClient _httpClient;
        //public CustomController(IHttpClientFactory factory)
        //{
        //    _httpClient = factory.CreateClient("ApiClient");
        //}

        //private async Task<IEnumerable<T>?> GetObjectListAsync<T>(string apiController)
        //{
        //    var response = await _httpClient.GetAsync(apiController);
        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
    }
}
