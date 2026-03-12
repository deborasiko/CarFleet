using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IFleetLocationRepository : IRepository<FleetLocation>
{
    Task<IEnumerable<FleetLocation>> GetActiveLocationsAsync();
}
