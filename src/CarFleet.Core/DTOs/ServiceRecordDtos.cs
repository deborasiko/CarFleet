namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class ServiceRecordCreateDto
{
    public int VehicleId { get; set; }
    public ServiceType ServiceType { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ServiceDate { get; set; }
    public int? Odometer { get; set; }
    public decimal Cost { get; set; }
    public string ServiceProvider { get; set; } = string.Empty;
    public DateTime? NextServiceDue { get; set; }
}

public class ServiceRecordUpdateDto
{
    public ServiceType? ServiceType { get; set; }
    public string? Description { get; set; }
    public DateTime? ServiceDate { get; set; }
    public int? Odometer { get; set; }
    public decimal? Cost { get; set; }
    public string? ServiceProvider { get; set; }
    public DateTime? NextServiceDue { get; set; }
}

public class ServiceRecordResponseDto
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
    public int DaysUntilNextService => NextServiceDue.HasValue ? (NextServiceDue.Value - DateTime.Now).Days : -1;
}
