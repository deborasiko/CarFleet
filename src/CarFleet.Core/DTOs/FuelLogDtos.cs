namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class FuelLogCreateDto
{
    public int VehicleId { get; set; }
    public int? DriverId { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal Liters { get; set; }
    public decimal PricePerLiter { get; set; }
    public int? Odometer { get; set; }
    public string FuelStation { get; set; } = string.Empty;
}

public class FuelLogUpdateDto
{
    public DateTime? FuelDate { get; set; }
    public decimal? Liters { get; set; }
    public decimal? PricePerLiter { get; set; }
    public int? Odometer { get; set; }
    public string? FuelStation { get; set; }
}

public class FuelLogResponseDto
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
}
