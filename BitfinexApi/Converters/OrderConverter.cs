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

            var order = new OrderItem();
            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();

            order.Symbol = reader.GetString().Replace("t", "");
            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();

            order.Amount = (float)reader.GetDouble();
            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();
            order.Status = reader.GetString().Split(" ")[0];
            reader.Read();
            reader.Read();
            reader.Read();
            order.Price = (float)reader.GetDouble();

            while (reader.Read())
            {
                if (reader.CurrentDepth == startDepth)
                    return order;
            }

            return order;
        }

        public override void Write(Utf8JsonWriter writer, OrderItem value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
