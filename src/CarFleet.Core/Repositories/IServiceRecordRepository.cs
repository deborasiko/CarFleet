using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IServiceRecordRepository : IRepository<ServiceRecord>
{
    Task<IEnumerable<ServiceRecord>> GetServiceRecordsForVehicleAsync(int vehicleId);
    Task<IEnumerable<ServiceRecord>> GetOverdueServiceRecordsAsync();
    Task<decimal> GetTotalMaintenanceCostAsync(int vehicleId);
}
