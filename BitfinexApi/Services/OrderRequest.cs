using BitfinexApi.Converters;
using BitfinexApi.Services.Interfaces;
using System.Text.Json.Serialization;

namespace BitfinexApi.Services
{
    class OrderRequest : IRequest
    {
        public string ApiPath => "v2/auth/r/orders/hist";

        public JsonConverter JsonConverter => new OrderConverter();
    }
}
