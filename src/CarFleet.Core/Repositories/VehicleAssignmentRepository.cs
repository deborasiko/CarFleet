using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class VehicleAssignmentRepository : Repository<VehicleAssignment>, IVehicleAssignmentRepository
{
    public VehicleAssignmentRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<VehicleAssignment>> GetActiveAssignmentsAsync()
    {
        return await DbSet
            .Where(va => va.EndDate == null || va.EndDate > DateTime.Now)
            .Include(va => va.Vehicle)
            .Include(va => va.Driver)
            .ToListAsync();
    }

    public async Task<VehicleAssignment?> GetActiveAssignmentForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(va => va.VehicleId == vehicleId && (va.EndDate == null || va.EndDate > DateTime.Now))
            .Include(va => va.Driver)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<VehicleAssignment>> GetAssignmentHistoryAsync(int vehicleId)
    {
        return await DbSet
            .Where(va => va.VehicleId == vehicleId)
            .Include(va => va.Driver)
            .OrderByDescending(va => va.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<VehicleAssignment>> GetDriverAssignmentsAsync(int driverId)
    {
        return await DbSet
            .Where(va => va.DriverId == driverId)
            .Include(va => va.Vehicle)
            .OrderByDescending(va => va.StartDate)
            .ToListAsync();
    }
}
