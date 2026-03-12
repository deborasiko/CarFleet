using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class ServiceRecordRepository : Repository<ServiceRecord>, IServiceRecordRepository
{
    public ServiceRecordRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<ServiceRecord>> GetServiceRecordsForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(sr => sr.VehicleId == vehicleId)
            .OrderByDescending(sr => sr.ServiceDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRecord>> GetOverdueServiceRecordsAsync()
    {
        return await DbSet
            .Where(sr => sr.NextServiceDue < DateTime.Now)
            .Include(sr => sr.Vehicle)
            .OrderBy(sr => sr.NextServiceDue)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalMaintenanceCostAsync(int vehicleId)
    {
        return await DbSet
            .Where(sr => sr.VehicleId == vehicleId)
            .SumAsync(sr => sr.Cost);
    }
}
