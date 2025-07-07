using MetaExchange.Application.DTOs;
using MetaExchange.Application.Interfaces;
using MetaExchange.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetaExchange.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExchangeController(IMetaExchangeService service, IExchangeRepository repository) : ControllerBase
{
    #region Public methods declaration

    [HttpPost]
    [ProducesResponseType(typeof(ExecutionPlan), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBestExecution([FromBody] BestExecutionRequest request)
    {
        var exchanges = await repository.GetAllAsync();
        var executionPlan =
            service.GetBestExecutionPlan(exchanges, request.OrderType, request.Amount);

        if (!executionPlan.Orders.Any())
            return NotFound("No execution orders could be generated for the given request.");

        return Ok(executionPlan);
    }

    #endregion
}