using BitfinexApi.Models;
using BitfinexApi.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BitfinexApi.Services
{
    class CzkRateService : ICzkRateService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string ExchangeUri = "https://api.exchangeratesapi.io/latest?base=USD&symbols=CZK";

        public CzkRateService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<float> Get()
        {
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(ExchangeUri);
            var exchange = JsonSerializer.Deserialize<Exchange>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return exchange!.Rates.CZK;
        }
    }
}
