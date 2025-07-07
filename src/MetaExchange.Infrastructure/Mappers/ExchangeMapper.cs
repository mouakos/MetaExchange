using MetaExchange.Domain.Entities;
using MetaExchange.Infrastructure.DTOs;

namespace MetaExchange.Infrastructure.mappers;

public static class ExchangeMapper
{
    public static Exchange ToDomain(ExchangeDto dto)
    {
        return new Exchange
        {
            Id = dto.Id,
            CryptoBalance = dto.AvailableFunds.Crypto,
            EuroBalance = dto.AvailableFunds.Euro,
            OrderBook = new OrderBook
            {
                Bids = dto.OrderBook.Bids.Select(ToOrder).ToList(),
                Asks = dto.OrderBook.Asks.Select(ToOrder).ToList()
            }
        };
    }

    private static Order ToOrder(OrderEntryDto dto)
    {
        return new Order
        {
            Id = Guid.Parse(dto.Order.Id),
            Time = dto.Order.Time,
            Type = Enum.TryParse<OrderType>(dto.Order.Type, true, out var type) ? type : OrderType.Buy,
            Amount = dto.Order.Amount,
            Price = dto.Order.Price
        };
    }
}