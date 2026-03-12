using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<Trip>> GetTripsForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(t => t.VehicleId == vehicleId)
            .OrderByDescending(t => t.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsForDriverAsync(int driverId)
    {
        return await DbSet
            .Where(t => t.DriverId == driverId)
            .OrderByDescending(t => t.StartTime)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalDistanceAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await DbSet
            .Where(t => t.VehicleId == vehicleId && t.StartTime >= startDate && t.StartTime <= endDate)
            .SumAsync(t => t.DistanceKm);
    }

    public async Task<Trip?> GetActiveTripsForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(t => t.VehicleId == vehicleId && t.EndTime == null)
            .FirstOrDefaultAsync();
    }
}
