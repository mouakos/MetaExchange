namespace MetaExchange.Infrastructure.DTOs;

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}