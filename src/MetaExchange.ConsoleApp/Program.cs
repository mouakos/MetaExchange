using MetaExchange.Application;
using MetaExchange.Application.Interfaces;
using MetaExchange.Domain.Entities;
using MetaExchange.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MetaExchange.ConsoleApp;

public class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration);
                services.AddApplication();
            })
            .Build();

        var scope = host.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IExchangeRepository>();
        var service = scope.ServiceProvider.GetRequiredService<IMetaExchangeService>();

        // ✅ Input loop for OrderType
        OrderType orderType;
        while (true)
        {
            Console.Write("Order Type (Buy/Sell): ");
            var input = Console.ReadLine();
            if (Enum.TryParse(input, ignoreCase: true, out orderType))
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Invalid order type. Please enter 'Buy' or 'Sell'.");
            Console.ResetColor();
        }

        // ✅ Input loop for Amount
        decimal amount;
        while (true)
        {
            Console.Write("Amount (BTC): ");
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out amount) && amount > 0)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid amount. Please enter a positive number.");
            Console.ResetColor();
        }

        var exchanges = await repository.GetAllAsync();
        var executionPlan = service.GetBestExecutionPlan(exchanges, orderType, amount);

        if (!executionPlan.Orders.Any())
        {
            Console.WriteLine("No execution orders could be generated for the the given inputs");
            return;
        }

        Console.WriteLine("\n Best Execution Plan:");
        foreach (var order in executionPlan.Orders)
        {
            Console.WriteLine(
                $"Exchange: {order.ExchangeId}, Type: {order.Type}, Price: {order.Price} EUR, Amount: {order.Amount} BTC");
        }

        Console.WriteLine($"Total Cost: {executionPlan.TotalCost} EUR, Total Amount: {executionPlan.TotalAmount} BTC");

        Console.WriteLine("Press any key to exit...");
    }
}