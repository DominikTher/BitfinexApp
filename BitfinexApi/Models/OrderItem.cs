namespace BitfinexApi.Models
{
    public class OrderItem
    {
        public string Symbol { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
    }
}
