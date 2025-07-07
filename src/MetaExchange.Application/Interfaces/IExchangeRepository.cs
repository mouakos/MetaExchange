using MetaExchange.Domain.Entities;

namespace MetaExchange.Application.Interfaces;

public interface IExchangeRepository
{
    Task<List<Exchange>> GetAllAsync();
}