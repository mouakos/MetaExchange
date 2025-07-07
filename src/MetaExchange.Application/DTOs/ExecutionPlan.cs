namespace MetaExchange.Application.DTOs;

public class ExecutionPlan
{
    public List<ExecutionOrder> Orders { get; set; } = new();
    public decimal TotalCost { get; set; }
    public decimal TotalAmount { get; set; }
}