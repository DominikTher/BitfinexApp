using System.Threading.Tasks;

namespace BitfinexApi.Services.Interfaces
{
    interface ICzkRateService
    {
        Task<float> Get();
    }
}