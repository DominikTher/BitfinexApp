using BitfinexApi.Converters;
using BitfinexApi.Services.Interfaces;
using System.Text.Json.Serialization;

namespace BitfinexApi.Services
{
    class WalletRequest : IRequest
    {
        public string ApiPath => "v2/auth/r/wallets";
        public JsonConverter JsonConverter => new WalletConverter();
    }
}
