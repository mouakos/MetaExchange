namespace MetaExchange.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Time { get; set; }
    public OrderType Type { get; set; }
    public OrderKind Kind { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}