using MetaExchange.Domain.Entities;
using MetaExchange.Application.DTOs;
using MetaExchange.Application.Interfaces;


namespace MetaExchange.Application.Services;

public class MetaExchangeService : IMetaExchangeService
{
    public ExecutionPlan GetBestExecutionPlan(List<Exchange> exchanges, OrderType orderType, decimal amount)
    {
        return orderType == OrderType.Buy
            ? GetBestBuyExecution(exchanges, amount)
            : GetBestSellExecution(exchanges, amount);
    }

    private static ExecutionPlan GetBestBuyExecution(List<Exchange> exchanges, decimal amount)
    {
        var allAsks = exchanges
            .SelectMany(e => e.OrderBook.Asks.Select(a => new { Exchange = e, Order = a }))
            .Where(x => x.Exchange.EuroBalance >= x.Order.Price * x.Order.Amount)
            .OrderBy(x => x.Order.Price)
            .ToList();

        var executionPlan = new ExecutionPlan();
        var remaining = amount;

        foreach (var ask in allAsks)
        {
            if (remaining <= 0) break;
            var maxBuyable = Math.Min(ask.Order.Amount, ask.Exchange.EuroBalance / ask.Order.Price);
            var buyAmount = Math.Min(maxBuyable, remaining);
            if (buyAmount <= 0) continue;
            executionPlan.Orders.Add(new ExecutionOrder
            {
                ExchangeId = ask.Exchange.Id,
                Type = OrderType.Buy,
                Price = ask.Order.Price,
                Amount = buyAmount
            });
            executionPlan.TotalCost += buyAmount * ask.Order.Price;
            executionPlan.TotalAmount += buyAmount;
            remaining -= buyAmount;
        }

        return executionPlan;
    }

    private static ExecutionPlan GetBestSellExecution(List<Exchange> exchanges, decimal amount)
    {
        var allBids = exchanges
            .SelectMany(e => e.OrderBook.Bids.Select(b => new { Exchange = e, Order = b }))
            .Where(x => x.Exchange.CryptoBalance >= x.Order.Amount)
            .OrderByDescending(x => x.Order.Price)
            .ToList();

        var executionPlan = new ExecutionPlan();
        var remaining = amount;

        foreach (var bid in allBids)
        {
            if (remaining <= 0) break;
            var sellAmount = Math.Min(bid.Order.Amount, Math.Min(bid.Exchange.CryptoBalance, remaining));
            if (sellAmount <= 0) continue;
            executionPlan.Orders.Add(new ExecutionOrder
            {
                ExchangeId = bid.Exchange.Id,
                Type = OrderType.Sell,
                Price = bid.Order.Price,
                Amount = sellAmount
            });
            executionPlan.TotalCost += sellAmount * bid.Order.Price;
            executionPlan.TotalAmount += sellAmount;
            remaining -= sellAmount;
        }

        return executionPlan;
    }
}