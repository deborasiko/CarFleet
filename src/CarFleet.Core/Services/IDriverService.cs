using CarFleet.Core.DTOs;
using CarFleet.Core.Models;

namespace CarFleet.Core.Services;

public interface IDriverService
{
    Task<DriverResponseDto?> GetDriverByIdAsync(int id);
    Task<IEnumerable<DriverResponseDto>> GetAllDriversAsync();
    Task<IEnumerable<DriverResponseDto>> SearchDriversAsync(string? searchTerm, DriverStatus? status);
    Task<IEnumerable<DriverResponseDto>> GetExpiredLicenseDriversAsync();
    Task<DriverResponseDto> CreateDriverAsync(DriverCreateDto driverDto);
    Task<DriverResponseDto?> UpdateDriverAsync(int id, DriverUpdateDto driverDto);
    Task<bool> DeleteDriverAsync(int id);
}
