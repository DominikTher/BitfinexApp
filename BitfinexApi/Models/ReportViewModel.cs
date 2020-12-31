using System.Collections.Generic;
using System.Linq;

namespace BitfinexApi.Models
{
    public class ReportViewModel
    {
        public float TotalUsd { get; init; }
        public float TotalCzk { get; init; }
        public float ProfitCzk { get; init; }
        public float Percentage { get; init; }
        public IEnumerable<WalletViewModel> WalletViews { get; init; } = Enumerable.Empty<WalletViewModel>();
    }
}
