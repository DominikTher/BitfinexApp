using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitfinexApi.Services.Interfaces
{
    interface IPriceCurrencyService
    {
        Task<IEnumerable<(string Currency, float Price)>> Get(IEnumerable<string> currency);
    }
}