namespace CarFleet.Core.DTOs;

public class TripCreateDto
{
    public int VehicleId { get; set; }
    public int? DriverId { get; set; }
    public DateTime StartTime { get; set; }
    public string StartLocation { get; set; } = string.Empty;
    public string EndLocation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
}

public class TripUpdateDto
{
    public DateTime? EndTime { get; set; }
    public decimal? DistanceKm { get; set; }
    public string? Purpose { get; set; }
}

public class TripResponseDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int? DriverId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string StartLocation { get; set; } = string.Empty;
    public string EndLocation { get; set; } = string.Empty;
    public decimal DistanceKm { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public bool IsActive => EndTime == null;
    public double? DurationHours => EndTime.HasValue ? (EndTime.Value - StartTime).TotalHours : null;
}
