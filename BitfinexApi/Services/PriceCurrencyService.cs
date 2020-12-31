using BitfinexApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BitfinexApi.Services
{
    class PriceCurrencyService : IPriceCurrencyService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string BitfinexPublicApiUri = "https://api-pub.bitfinex.com/";

        public PriceCurrencyService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<(string Currency, float Price)>> Get(IEnumerable<string> currency)
        {
            Func<string, Task<(string Currency, float Price)>> selectPriceCurrency
                = async currency =>
                {
                    if (currency == "USD") return ("USD", 1); // TODO constants

                    var httpClient = httpClientFactory.CreateClient();
                    var tickerUri = $"v2/ticker/t{currency}USD";
                    var response = await httpClient.GetStringAsync($"{BitfinexPublicApiUri}{tickerUri}");
                    var content = JsonSerializer.Deserialize<float[]>(response);
                    return (Currency: currency, Price: content![2]);
                };

            var priceCurrenciesTasks = currency.Select(selectPriceCurrency);
            var priceCurrencies = await Task.WhenAll(priceCurrenciesTasks);

            return priceCurrencies;
        }
    }
}
