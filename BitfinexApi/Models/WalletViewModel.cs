namespace BitfinexApi.Models
{
    public class WalletViewModel
    {
        public string Type { get; init; } = string.Empty;
        public string Currency { get; init; } = string.Empty;
        public string Balance { get; init; } = string.Empty;
        public string UnsettledInterest { get; init; } = string.Empty;
        public string AvaliableBalance { get; init; } = string.Empty;
        public float ActualPrice { get; init; }
        public float TotalUsd { get; init; }
        public float? Percentage { get; set; }
    }
}
