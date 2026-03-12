namespace CarFleet.Core.Models;

public enum ExpenseType
{
    Insurance,
    Maintenance,
    Tolls,
    Parking,
    Repairs,
    Cleaning,
    Registration,
    Other
}

public class Expense
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Vendor { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigation property
    public virtual Vehicle Vehicle { get; set; } = null!;
}
