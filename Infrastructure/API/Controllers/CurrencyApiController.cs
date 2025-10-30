using Microsoft.AspNetCore.Mvc;
using ProductManagment.Infrastructure.API.Models;
using System.Text.Json;

namespace ProductManagment.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyApiController : ControllerBase
    {
        private static decimal? offRate;
        private readonly HttpClient _httpClient = new();

        [HttpGet]
        public async Task<string> Get(decimal value)
        {
            if (value == 0) return "0.000";

            try
            {
                var json = await _httpClient.GetStringAsync("https://api.nbrb.by/exrates/rates/431");

                Rate? rate = JsonSerializer.Deserialize<Rate>(json);
                offRate = rate?.Cur_OfficialRate;

                if (offRate == null) return "0.000";
                if (offRate == 0) return "0.000";
                else
                {
                    var usd = (value / offRate).ToString()!;
                    return usd;
                }
            }
            catch (Exception) { return "0.000"; }
        }
    }
}
