namespace MetaExchange.Infrastructure.DTOs;

public class ExchangeDto
{
    public string Id { get; set; } = string.Empty;
    public AvailableFundsDto AvailableFunds { get; set; } = new();
    public OrderBookDto OrderBook { get; set; } = new();
}