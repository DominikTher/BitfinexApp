using BitfinexApi.Extensions;
using BitfinexApi.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitfinexApi.Converters
{
    class OrderConverter : JsonConverter<OrderItem>
    {
        public override OrderItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var startDepth = reader.CurrentDepth;

            var order = new OrderItem(); // TODO read to variables and then create object with init properties
            reader.Read(4);
            order.Symbol = reader.GetString()!.Replace("t", "");
            reader.Read(4);
            order.Amount = (float)reader.GetDouble();
            reader.Read(6);
            order.Status = reader.GetString()!.Split(" ")[0];
            reader.Read(3);
            order.Price = (float)reader.GetDouble();

            if (reader.ReadToEnd(startDepth))
                return order;

            return order;
        }

        public override void Write(Utf8JsonWriter writer, OrderItem value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
