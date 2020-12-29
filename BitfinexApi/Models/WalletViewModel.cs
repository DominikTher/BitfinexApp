namespace BitfinexApi.Models
{
    public class WalletViewModel
    {
        public string Type { get; init; }
        public string Currency { get; init; }
        public string Balance { get; init; }
        public string UnsettledInterest { get; init; }
        public string AvaliableBalance { get; init; }
        public float ActualPrice { get; init; }
        public float TotalUsd { get; init; }
        public float? Percentage { get; set; }
    }
}
