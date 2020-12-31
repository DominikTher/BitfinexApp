namespace BitfinexApi.Models
{
    public class OrderItem
    {
        public string Symbol { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public float Price { get; set; }
    }
}
