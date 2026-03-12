using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class FleetLocationRepository : Repository<FleetLocation>, IFleetLocationRepository
{
    public FleetLocationRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<FleetLocation>> GetActiveLocationsAsync()
    {
        return await DbSet
            .Where(fl => fl.IsActive)
            .ToListAsync();
    }
}
