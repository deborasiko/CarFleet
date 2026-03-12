using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IDriverRepository : IRepository<Driver>
{
    Task<IEnumerable<Driver>> SearchDriversAsync(string? searchTerm, DriverStatus? status);
    Task<IEnumerable<Driver>> GetExpiredLicenseDriversAsync();
    Task<Driver?> GetDriverWithAssignmentsAsync(int driverId);
}
