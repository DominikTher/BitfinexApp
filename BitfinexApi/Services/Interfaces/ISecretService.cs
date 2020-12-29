using System.Collections.Generic;

namespace BitfinexApi.Services.Interfaces
{
    public interface ISecretService
    {
        IDictionary<string, string> BuildSecretHeaders(string apiPath);
    }
}