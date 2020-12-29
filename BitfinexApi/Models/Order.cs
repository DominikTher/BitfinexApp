namespace BitfinexApi.Models
{
    class Order
    {
        public string Symbol { get; init; }
        public float Amount { get; init; }
        public float Total { get; set; }
    }
}
