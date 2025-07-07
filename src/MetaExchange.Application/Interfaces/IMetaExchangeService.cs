using MetaExchange.Application.DTOs;
using MetaExchange.Domain.Entities;

namespace MetaExchange.Application.Interfaces;

public interface IMetaExchangeService
{
    ExecutionPlan GetBestExecutionPlan(List<Exchange> exchanges, OrderType orderType, decimal amount);
}