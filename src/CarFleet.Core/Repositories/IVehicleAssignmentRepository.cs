using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IVehicleAssignmentRepository : IRepository<VehicleAssignment>
{
    Task<IEnumerable<VehicleAssignment>> GetActiveAssignmentsAsync();
    Task<VehicleAssignment?> GetActiveAssignmentForVehicleAsync(int vehicleId);
    Task<IEnumerable<VehicleAssignment>> GetAssignmentHistoryAsync(int vehicleId);
    Task<IEnumerable<VehicleAssignment>> GetDriverAssignmentsAsync(int driverId);
}
