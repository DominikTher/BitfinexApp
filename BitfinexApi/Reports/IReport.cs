using BitfinexApi.Models;
using System.Threading.Tasks;

namespace BitfinexApi.Reports
{
    public interface IReport
    {
        Task<ReportViewModel> Execute();
    }
}