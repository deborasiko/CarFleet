using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class FleetLocationService : IFleetLocationService
{
    private readonly IFleetLocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public FleetLocationService(IFleetLocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<FleetLocationResponseDto?> GetLocationByIdAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        return location == null ? null : _mapper.Map<FleetLocationResponseDto>(location);
    }

    public async Task<IEnumerable<FleetLocationResponseDto>> GetAllLocationsAsync()
    {
        var locations = await _locationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FleetLocationResponseDto>>(locations);
    }

    public async Task<IEnumerable<FleetLocationResponseDto>> GetActiveLocationsAsync()
    {
        var locations = await _locationRepository.GetActiveLocationsAsync();
        return _mapper.Map<IEnumerable<FleetLocationResponseDto>>(locations);
    }

    public async Task<FleetLocationResponseDto> CreateLocationAsync(FleetLocationCreateDto locationDto)
    {
        var location = _mapper.Map<FleetLocation>(locationDto);
        location.IsActive = true;
        await _locationRepository.AddAsync(location);
        await _locationRepository.SaveChangesAsync();
        return _mapper.Map<FleetLocationResponseDto>(location);
    }

    public async Task<FleetLocationResponseDto?> UpdateLocationAsync(int id, FleetLocationUpdateDto locationDto)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null) return null;

        _mapper.Map(locationDto, location);
        _locationRepository.Update(location);
        await _locationRepository.SaveChangesAsync();
        return _mapper.Map<FleetLocationResponseDto>(location);
    }

    public async Task<bool> DeleteLocationAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null) return false;

        _locationRepository.Remove(location);
        await _locationRepository.SaveChangesAsync();
        return true;
    }
}
