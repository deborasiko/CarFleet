namespace CarFleet.Core.Models;

public enum DriverStatus
{
    Active,
    Inactive,
    OnLeave,
    Retired
}

public class Driver
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public DriverStatus Status { get; set; } = DriverStatus.Active;

    // Navigation properties
    public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; } = new List<VehicleAssignment>();
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    public virtual ICollection<FuelLog> FuelLogs { get; set; } = new List<FuelLog>();
    public virtual User? User { get; set; }
}
