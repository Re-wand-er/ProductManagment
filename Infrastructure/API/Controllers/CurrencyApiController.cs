using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Interfaces;
using ProductManagment.Infrastructure.API.Models;
using System.Text.Json;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyApiController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyApiController> _logger;

        public CurrencyApiController(ICurrencyService currencyService, ILogger<CurrencyApiController> logger) 
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get(decimal value)
        {
            _logger.LogInformation($"Get /CurrencyApi/Get: Value={value}");
            var convert = await _currencyService.ConvertToUsd(value);
            return convert.ToString("F3"); // 1234.000
        }
    }
}
