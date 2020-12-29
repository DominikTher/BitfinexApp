using BitfinexApi.Configurations;
using BitfinexApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BitfinexApi.Services
{
    class SecretService : ISecretService
    {
        private readonly BitfinexApiConfiguration bitfinexApiConfiguration;
        private readonly DateTime epoch = new DateTime(1970, 1, 1);

        public SecretService(IOptions<BitfinexApiConfiguration> bitfinexApiConfigurationOptions)
        {
            bitfinexApiConfiguration = bitfinexApiConfigurationOptions.Value;
        }

        public IDictionary<string, string> BuildSecretHeaders(string apiPath)
        {
            var nonce = ((int)(DateTime.UtcNow - epoch).TotalSeconds).ToString();
            var fullSecretPath = $"/api/{apiPath}{nonce}";
            var hash = new HMACSHA384(Encoding.UTF8.GetBytes(bitfinexApiConfiguration.Secret))
                .ComputeHash(Encoding.UTF8.GetBytes(fullSecretPath));

            return new Dictionary<string, string>
            {
                { "bfx-nonce", nonce },
                { "bfx-apikey", bitfinexApiConfiguration.ApiKey },
                { "bfx-signature",  GetHexString(hash) }
            };
        }

        private string GetHexString(byte[] bytes)
        {
            var stringBuilder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                stringBuilder.Append(string.Format("{0:x2}", b));
            }

            return stringBuilder.ToString();
        }
    }
}
