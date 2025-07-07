using System.ComponentModel.DataAnnotations;
using MetaExchange.Domain.Entities;

namespace MetaExchange.WebApi.Models;

public record BestExecutionRequest(
    [Range(0.0001, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    decimal Amount,
    OrderType OrderType);