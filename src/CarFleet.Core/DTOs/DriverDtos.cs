namespace CarFleet.Core.DTOs;

using CarFleet.Core.Models;

public class DriverCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}

public class DriverUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DriverStatus? Status { get; set; }
}

public class DriverResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public DriverStatus Status { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public int DaysUntilLicenseExpiry => (LicenseExpiryDate - DateTime.Now).Days;
}
