namespace CarFleet.Core.DTOs;

public class VehicleAssignmentCreateDto
{
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public DateTime StartDate { get; set; }
    public string AssignedBy { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class VehicleAssignmentUpdateDto
{
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }
}

public class VehicleAssignmentResponseDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string AssignedBy { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsActive => EndDate == null || EndDate > DateTime.Now;
    public VehicleResponseDto? Vehicle { get; set; }
    public DriverResponseDto? Driver { get; set; }
}
