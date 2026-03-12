using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<VehicleResponseDto?> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle == null ? null : _mapper.Map<VehicleResponseDto>(vehicle);
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VehicleResponseDto>>(vehicles);
    }

    public async Task<IEnumerable<VehicleResponseDto>> SearchVehiclesAsync(string? searchTerm, int? year, string? make, VehicleStatus? status)
    {
        var vehicles = await _vehicleRepository.SearchVehiclesAsync(searchTerm, year, make, status);
        return _mapper.Map<IEnumerable<VehicleResponseDto>>(vehicles);
    }

    public async Task<VehicleResponseDto> CreateVehicleAsync(VehicleCreateDto vehicleDto)
    {
        var vehicle = _mapper.Map<Vehicle>(vehicleDto);
        vehicle.Status = VehicleStatus.Active;
        await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.SaveChangesAsync();
        return _mapper.Map<VehicleResponseDto>(vehicle);
    }

    public async Task<VehicleResponseDto?> UpdateVehicleAsync(int id, VehicleUpdateDto vehicleDto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return null;

        _mapper.Map(vehicleDto, vehicle);
        _vehicleRepository.Update(vehicle);
        await _vehicleRepository.SaveChangesAsync();
        return _mapper.Map<VehicleResponseDto>(vehicle);
    }

    public async Task<bool> DeleteVehicleAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return false;

        _vehicleRepository.Remove(vehicle);
        await _vehicleRepository.SaveChangesAsync();
        return true;
    }
}
