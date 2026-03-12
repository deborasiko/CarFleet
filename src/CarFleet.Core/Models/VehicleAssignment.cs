namespace CarFleet.Core.Models;

public class VehicleAssignment
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string AssignedBy { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    // Navigation properties
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Driver Driver { get; set; } = null!;
}
