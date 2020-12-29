using System.Collections.Generic;
using System.Net.Http;

namespace BitfinexApi.Extensions
{
    static class HttpClientExtensions
    {
        public static void ApplyCustomHeaders(this HttpClient httpClient, IDictionary<string, string> customHeaders)
        {
            foreach (var customHeader in customHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
            }
        }
    }
}
