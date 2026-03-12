using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<Driver>> SearchDriversAsync(string? searchTerm, DriverStatus? status)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(d => d.FirstName.Contains(searchTerm) || d.LastName.Contains(searchTerm) || d.Email.Contains(searchTerm));
        }

        if (status.HasValue)
        {
            query = query.Where(d => d.Status == status);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Driver>> GetExpiredLicenseDriversAsync()
    {
        return await DbSet.Where(d => d.LicenseExpiryDate < DateTime.Now).ToListAsync();
    }

    public async Task<Driver?> GetDriverWithAssignmentsAsync(int driverId)
    {
        return await DbSet
            .Include(d => d.VehicleAssignments)
            .ThenInclude(va => va.Vehicle)
            .FirstOrDefaultAsync(d => d.Id == driverId);
    }
}
