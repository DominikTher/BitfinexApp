using System.Text.Json.Serialization;

namespace BitfinexApi.Services.Interfaces
{
    interface IRequest
    {
        string ApiPath { get; }
        JsonConverter JsonConverter { get; }
    }
}