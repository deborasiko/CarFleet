using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface ITripRepository : IRepository<Trip>
{
    Task<IEnumerable<Trip>> GetTripsForVehicleAsync(int vehicleId);
    Task<IEnumerable<Trip>> GetTripsForDriverAsync(int driverId);
    Task<decimal> GetTotalDistanceAsync(int vehicleId, DateTime startDate, DateTime endDate);
    Task<Trip?> GetActiveTripsForVehicleAsync(int vehicleId);
}
