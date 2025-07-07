namespace MetaExchange.Domain.Entities;

public class Exchange
{
     public string Id { get; set; } = string.Empty;
    public decimal CryptoBalance { get; set; }
    public decimal EuroBalance { get; set; }
    public OrderBook OrderBook { get; set; } = new();
}