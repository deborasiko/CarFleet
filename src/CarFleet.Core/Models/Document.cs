namespace CarFleet.Core.Models;

public enum DocumentType
{
    Insurance,
    Registration,
    Inspection,
    EmissionTest,
    VehicleTitle,
    MaintenanceRecord,
    Other
}

public class Document
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Vehicle Vehicle { get; set; } = null!;
}
