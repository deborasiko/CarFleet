using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string? searchTerm, int? year, string? make, VehicleStatus? status);
    Task<IEnumerable<Vehicle>> GetVehiclesByFleetLocationAsync(int fleetLocationId);
    Task<Vehicle?> GetVehicleWithAssignmentsAsync(int vehicleId);
}
