namespace CarFleet.Core.Models;

public enum ServiceType
{
    Maintenance,
    Repair,
    Inspection,
    Tire,
    Oil,
    Transmission,
    Engine,
    Other
}

public class ServiceRecord
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public ServiceType ServiceType { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ServiceDate { get; set; }
    public int? Odometer { get; set; }
    public decimal Cost { get; set; }
    public string ServiceProvider { get; set; } = string.Empty;
    public DateTime? NextServiceDue { get; set; }

    // Navigation property
    public virtual Vehicle Vehicle { get; set; } = null!;
}
