namespace MetaExchange.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public OrderType Type { get; set; }
    public string Kind { get; set; } = string.Empty; 
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}