using CarFleet.Core.DTOs;
using CarFleet.Core.Models;

namespace CarFleet.Core.Services;

public interface IVehicleService
{
    Task<VehicleResponseDto?> GetVehicleByIdAsync(int id);
    Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync();
    Task<IEnumerable<VehicleResponseDto>> SearchVehiclesAsync(string? searchTerm, int? year, string? make, VehicleStatus? status);
    Task<VehicleResponseDto> CreateVehicleAsync(VehicleCreateDto vehicleDto);
    Task<VehicleResponseDto?> UpdateVehicleAsync(int id, VehicleUpdateDto vehicleDto);
    Task<bool> DeleteVehicleAsync(int id);
}
