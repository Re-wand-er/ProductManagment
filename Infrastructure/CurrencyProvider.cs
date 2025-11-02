using ProductManagment.Application.Interfaces;
using ProductManagment.Infrastructure.API.Models;

namespace ProductManagment.Infrastructure
{
    public class CurrencyProvider : ICurrencyProvider
    {
        private readonly HttpClient _httpClient;

        public CurrencyProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal?> GetRateAsync()
        {
            try
            {
                var rate = await _httpClient.GetFromJsonAsync<Rate>("https://api.nbrb.by/exrates/rates/431");
                return rate?.Cur_OfficialRate;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
