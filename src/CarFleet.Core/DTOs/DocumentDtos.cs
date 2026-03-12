namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class DocumentCreateDto
{
    public int VehicleId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class DocumentUpdateDto
{
    public DocumentType? DocumentType { get; set; }
    public string? FilePath { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class DocumentResponseDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime UploadedAt { get; set; }
    public bool IsExpired => ExpiryDate != null && ExpiryDate < DateTime.Now;
    public int? DaysUntilExpiry => ExpiryDate.HasValue ? (ExpiryDate.Value - DateTime.Now).Days : null;
}
