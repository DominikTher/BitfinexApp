namespace BitfinexApi.Models
{
    class Order
    {
        public string Symbol { get; init; } = string.Empty;
        public float Amount { get; init; }
        public float Total { get; init; }
    }
}
