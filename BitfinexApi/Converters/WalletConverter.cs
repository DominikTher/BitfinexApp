using BitfinexApi.Extensions;
using BitfinexApi.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitfinexApi.Converters
{
    class WalletConverter : JsonConverter<Wallet>
    {
        public override Wallet Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var startDepth = reader.CurrentDepth;

            var wallet = new Wallet();
            reader.Read();
            wallet.Type = reader.GetString()!;
            reader.Read();
            wallet.Currency = reader.GetString()!;
            reader.Read();
            wallet.Balance = (float)reader.GetDouble();
            reader.Read();
            wallet.UnsettledInterest = (float)reader.GetDouble();
            reader.Read();
            wallet.AvaliableBalance = (float)reader.GetDouble();

            if (reader.ReadToEnd(startDepth))
                return wallet;

            return wallet;
        }

        public override void Write(Utf8JsonWriter writer, Wallet value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
