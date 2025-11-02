using ProductManagment.Application.Interfaces;
using ProductManagment.Infrastructure.API.Controllers;
using ProductManagment.Infrastructure.API.Models;
using System.Net.Http;
using System.Text.Json;

namespace ProductManagment.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyProvider _currencyProvider;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(ICurrencyProvider currencyProvider, ILogger<CurrencyService> logger)
        {
            _currencyProvider = currencyProvider;
            _logger = logger;
        }
        public async Task<decimal> ConvertToUsd(decimal value) 
        {
            var rate = await _currencyProvider.GetRateAsync();
            if (!rate.HasValue)
            {
                _logger.LogInformation("Ошибка получения информации с внешнего API!");
                return 0;
            }

            // Nullable<decimal> rate
            return value / rate.Value;
        }
    }
}
