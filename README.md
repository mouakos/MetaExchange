# Meta Exchange

This solution implements a meta crypto exchange using Clean Architecture in C#. It consists of:
- Domain, Application, Infrastructure, ConsoleApp, and WebApi projects
- Reads order books from JSON files and computes the best execution plan for BTC/EUR trades

## Projects
- **CryptoExchange.Domain**: Core entities and business rules
- **CryptoExchange.Application**: Use cases and application logic
- **CryptoExchange.Infrastructure**: File I/O, JSON parsing, and external dependencies
- **CryptoExchange.ConsoleApp**: Console interface for running the meta exchange
- **CryptoExchange.WebApi**: ASP.NET Core Web API exposing the meta exchange functionality

## Getting Started
1. Build the solution: `dotnet build`
2. Run the console app: `dotnet run --project ./CryptoExchange.ConsoleApp`
3. Run the Web API: `dotnet run --project ./CryptoExchange.WebApi`

## Exchange Data Folder
- **Important:** Set the path to your exchange data folder (containing the JSON files) in `appsettings.json` under a key like `ExchangeData:DirectoryPath` for the Web API and ConsoleApp.
- The application will use this folder to load order book data for all exchanges.

## Clean Architecture
This solution follows Clean Architecture principles. Business logic is isolated from infrastructure and UI concerns.