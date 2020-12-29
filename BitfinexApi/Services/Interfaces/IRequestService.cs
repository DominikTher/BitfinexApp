using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitfinexApi.Services.Interfaces
{
    interface IRequestService
    {
        Task<IEnumerable<T>> Execute<T>(IRequest request) where T : class;
    }
}