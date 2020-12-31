namespace BitfinexApi.Models
{
    public class Wallet
    {
        public string Type { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public float Balance { get; set; }
        public float UnsettledInterest { get; set; }
        public float AvaliableBalance { get; set; }
    }
}
