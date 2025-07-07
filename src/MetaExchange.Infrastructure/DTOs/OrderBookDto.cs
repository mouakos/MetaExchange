namespace MetaExchange.Infrastructure.DTOs;

public class OrderBookDto
{
    public List<OrderEntryDto> Bids { get; set; } = new();
    public List<OrderEntryDto> Asks { get; set; } = new();
}