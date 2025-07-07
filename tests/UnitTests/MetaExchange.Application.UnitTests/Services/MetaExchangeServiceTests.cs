using MetaExchange.Application.Services;
using MetaExchange.Domain.Entities;

namespace MetaExchange.Application.UnitTests.Services;

public class MetaExchangeServiceTests
{
    [Fact]
    public void GetBestExecution_Buy_ReturnsBestAskOrders()
    {
        // Arrange
        var exchanges = new List<Exchange>
        {
            new()
            {
                Id = "ex1",
                CryptoBalance = 10,
                EuroBalance = 100000,
                OrderBook = new OrderBook
                {
                    Asks =
                    [
                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Sell, Amount = 0.5m,
                            Price = 50000
                        },

                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Sell, Amount = 0.5m,
                            Price = 51000
                        }
                    ],
                    Bids = []
                }
            },
            new()
            {
                Id = "ex2",
                CryptoBalance = 5,
                EuroBalance = 50000,
                OrderBook = new OrderBook
                {
                    Asks =
                    [
                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Sell, Amount = 0.5m,
                            Price = 49500
                        }
                    ],
                    Bids = []
                }
            }
        };
        var service = new MetaExchangeService();

        // Act
        var executionPlan = service.GetBestExecutionPlan(exchanges, OrderType.Buy, 0.7m);

        // Assert
        Assert.NotNull(executionPlan);
        Assert.True(executionPlan.Orders.Count > 0);
        Assert.Equal(0.7m, executionPlan.TotalAmount);
        Assert.Equal("ex2", executionPlan.Orders[0].ExchangeId); // Best price first
        Assert.Equal(49500, executionPlan.Orders[0].Price);
    }

    [Fact]
    public void GetBestExecution_Sell_ReturnsBestBidOrders()
    {
        // Arrange
        var exchanges = new List<Exchange>
        {
            new()
            {
                Id = "ex1",
                CryptoBalance = 1,
                EuroBalance = 10000,
                OrderBook = new OrderBook
                {
                    Bids =
                    [
                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Buy, Amount = 0.5m, Price = 52000
                        },

                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Buy, Amount = 0.5m, Price = 51000
                        }
                    ],
                    Asks = []
                }
            },
            new()
            {
                Id = "ex2",
                CryptoBalance = 2,
                EuroBalance = 20000,
                OrderBook = new OrderBook
                {
                    Bids =
                    [
                        new Order
                        {
                            Id = Guid.NewGuid(), Time = DateTime.Now, Type = OrderType.Buy, Amount = 0.5m, Price = 53000
                        }
                    ],
                    Asks = []
                }
            }
        };
        var service = new MetaExchangeService();

        // Act
        var executionPlan = service.GetBestExecutionPlan(exchanges, OrderType.Sell, 0.7m);

        // Assert
        Assert.NotNull(executionPlan);
        Assert.True(executionPlan.Orders.Count > 0);
        Assert.Equal(0.7m, executionPlan.TotalAmount);
        Assert.Equal("ex2", executionPlan.Orders[0].ExchangeId); // Best price first
        Assert.Equal(53000, executionPlan.Orders[0].Price);
    }

    [Fact]
    public void GetBestExecution_NoOrders_ReturnsEmptyPlan()
    {
        // Arrange
        var exchanges = new List<Exchange>();
        var service = new MetaExchangeService();

        // Act
        var executionPlan = service.GetBestExecutionPlan(exchanges, OrderType.Buy, 1m);

        // Assert
        Assert.NotNull(executionPlan);
        Assert.Empty(executionPlan.Orders);
        Assert.Equal(0, executionPlan.TotalAmount);
        Assert.Equal(0, executionPlan.TotalCost);
    }
}