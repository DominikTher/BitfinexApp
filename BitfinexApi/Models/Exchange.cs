namespace BitfinexApi.Models
{
    class Exchange
    {
        public Rate Rates { get; init; } = new Rate();
        public string Base { get; init; } = string.Empty;
        public string Date { get; init; } = string.Empty;
    }
}
