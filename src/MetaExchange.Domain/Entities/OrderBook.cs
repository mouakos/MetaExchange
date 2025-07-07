namespace MetaExchange.Domain.Entities;

public class OrderBook
{
    public List<Order> Bids { get; set; } = new();
    public List<Order> Asks { get; set; } = new();
}