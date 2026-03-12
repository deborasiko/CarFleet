namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class VehicleCreateDto
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int? FleetLocationId { get; set; }
}

public class VehicleUpdateDto
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Vin { get; set; }
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public string? Color { get; set; }
    public FuelType? FuelType { get; set; }
    public VehicleStatus? Status { get; set; }
    public int? FleetLocationId { get; set; }
    public int? Odometer { get; set; }
}

public class VehicleResponseDto
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public VehicleStatus Status { get; set; }
    public int? FleetLocationId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int? Odometer { get; set; }
}
