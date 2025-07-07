using MetaExchange.Domain.Entities;

namespace CryptoExchange.Application.Interfaces;

public interface IExchangeRepository
{
    Task<List<Exchange>> GetAllAsync();
}