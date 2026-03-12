namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class ExpenseCreateDto
{
    public int VehicleId { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Vendor { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ExpenseUpdateDto
{
    public ExpenseType? ExpenseType { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? ExpenseDate { get; set; }
    public string? Vendor { get; set; }
    public string? Description { get; set; }
}

public class ExpenseResponseDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Vendor { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
