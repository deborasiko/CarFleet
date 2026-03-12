using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string? searchTerm, int? year, string? make, VehicleStatus? status)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(v => v.Make.Contains(searchTerm) || v.Model.Contains(searchTerm) || v.LicensePlate.Contains(searchTerm));
        }

        if (year.HasValue)
        {
            query = query.Where(v => v.Year == year);
        }

        if (!string.IsNullOrEmpty(make))
        {
            query = query.Where(v => v.Make == make);
        }

        if (status.HasValue)
        {
            query = query.Where(v => v.Status == status);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByFleetLocationAsync(int fleetLocationId)
    {
        return await DbSet.Where(v => v.FleetLocationId == fleetLocationId).ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleWithAssignmentsAsync(int vehicleId)
    {
        return await DbSet
            .Include(v => v.VehicleAssignments)
            .ThenInclude(va => va.Driver)
            .FirstOrDefaultAsync(v => v.Id == vehicleId);
    }
}
