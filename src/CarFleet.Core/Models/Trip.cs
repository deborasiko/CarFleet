namespace CarFleet.Core.Models;

public class Trip
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

    // Navigation properties
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Driver? Driver { get; set; }
}
