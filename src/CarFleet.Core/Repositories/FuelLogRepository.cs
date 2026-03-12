using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class FuelLogRepository : Repository<FuelLog>, IFuelLogRepository
{
    public FuelLogRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<FuelLog>> GetFuelLogsForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(fl => fl.VehicleId == vehicleId)
            .OrderByDescending(fl => fl.FuelDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalFuelCostAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await DbSet
            .Where(fl => fl.VehicleId == vehicleId && fl.FuelDate >= startDate && fl.FuelDate <= endDate)
            .SumAsync(fl => fl.TotalCost);
    }

    public async Task<decimal> GetAverageFuelConsumptionAsync(int vehicleId)
    {
        var logs = await DbSet
            .Where(fl => fl.VehicleId == vehicleId)
            .OrderBy(fl => fl.FuelDate)
            .ToListAsync();

        if (logs.Count < 2) return 0;

        decimal totalDistance = 0;
        decimal totalLiters = 0;

        for (int i = 1; i < logs.Count; i++)
        {
            if (logs[i].Odometer.HasValue && logs[i - 1].Odometer.HasValue)
            {
                totalDistance += logs[i].Odometer!.Value - logs[i - 1].Odometer!.Value;
                totalLiters += logs[i].Liters;
            }
        }

        return totalLiters > 0 ? totalDistance / totalLiters : 0;
    }
}
