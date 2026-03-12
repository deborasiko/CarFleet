namespace CarFleet.Core.Models;

public enum RoleType
{
    Admin,
    FleetManager,
    Driver
}

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RoleType RoleType { get; set; }
    public string Description { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
