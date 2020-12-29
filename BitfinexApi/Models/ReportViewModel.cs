using System.Collections.Generic;

namespace BitfinexApi.Models
{
    public class ReportViewModel
    {
        public float TotalUsd { get; set; }
        public float TotalCzk { get; set; }
        public float ProfitCzk { get; set; }
        public float Percentage { get; set; }
        public IEnumerable<WalletViewModel> WalletViews { get; set; }
    }
}
