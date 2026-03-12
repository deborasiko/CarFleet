namespace CarFleet.Core.Models;

public enum FuelType
{
    Diesel,
    Petrol,
    Electric
}

public enum VehicleStatus
{
    Active,
    Inactive,
    UnderMaintenance,
    Retired
}

public class Vehicle
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.Active;
    public int? FleetLocationId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int? Odometer { get; set; }

    // Navigation properties
    public virtual FleetLocation? FleetLocation { get; set; }
    public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; } = new List<VehicleAssignment>();
    public virtual ICollection<FuelLog> FuelLogs { get; set; } = new List<FuelLog>();
    public virtual ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}