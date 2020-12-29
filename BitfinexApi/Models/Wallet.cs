namespace BitfinexApi.Models
{
    public class Wallet
    {
        public string Type { get; set; }
        public string Currency { get; set; }
        public float Balance { get; set; }
        public float UnsettledInterest { get; set; }
        public float AvaliableBalance { get; set; }
    }
}
