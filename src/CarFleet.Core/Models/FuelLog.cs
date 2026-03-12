namespace CarFleet.Core.Models;

public class FuelLog
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int? DriverId { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal Liters { get; set; }
    public decimal PricePerLiter { get; set; }
    public decimal TotalCost { get; set; }
    public int? Odometer { get; set; }
    public string FuelStation { get; set; } = string.Empty;

    // Navigation properties
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Driver? Driver { get; set; }
}
