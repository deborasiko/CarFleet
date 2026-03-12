using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IFuelLogRepository : IRepository<FuelLog>
{
    Task<IEnumerable<FuelLog>> GetFuelLogsForVehicleAsync(int vehicleId);
    Task<decimal> GetTotalFuelCostAsync(int vehicleId, DateTime startDate, DateTime endDate);
    Task<decimal> GetAverageFuelConsumptionAsync(int vehicleId);
}
