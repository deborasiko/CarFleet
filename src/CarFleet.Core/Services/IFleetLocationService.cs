using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IFleetLocationService
{
    Task<FleetLocationResponseDto?> GetLocationByIdAsync(int id);
    Task<IEnumerable<FleetLocationResponseDto>> GetAllLocationsAsync();
    Task<IEnumerable<FleetLocationResponseDto>> GetActiveLocationsAsync();
    Task<FleetLocationResponseDto> CreateLocationAsync(FleetLocationCreateDto locationDto);
    Task<FleetLocationResponseDto?> UpdateLocationAsync(int id, FleetLocationUpdateDto locationDto);
    Task<bool> DeleteLocationAsync(int id);
}
