namespace CarFleet.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    public int? DriverId { get; set; }
    public int RoleId { get; set; }

    // Navigation properties
    public virtual Role Role { get; set; } = null!;
    public virtual Driver? Driver { get; set; }
}
