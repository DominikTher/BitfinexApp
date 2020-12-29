using BitfinexApi.Extensions;
using BitfinexApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BitfinexApi.Services
{
    class RequestService : IRequestService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ISecretService secretService;

        public RequestService(
            IHttpClientFactory httpClientFactory,
            ISecretService secretService)
        {
            this.httpClientFactory = httpClientFactory;
            this.secretService = secretService;
        }

        public async Task<IEnumerable<T>> Execute<T>(IRequest request)
            where T : class
        {
            var httpClient = httpClientFactory.CreateClient("bitfinex");
            httpClient.ApplyCustomHeaders(secretService.BuildSecretHeaders(request.ApiPath));
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}{request.ApiPath}", null!);
            var content = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(content, new JsonSerializerOptions
            {
                Converters = { request.JsonConverter }
            }) ?? Enumerable.Empty<T>();
        }
    }
}
