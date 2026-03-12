using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IFuelLogService
{
    Task<FuelLogResponseDto?> GetFuelLogByIdAsync(int id);
    Task<IEnumerable<FuelLogResponseDto>> GetAllFuelLogsAsync();
    Task<IEnumerable<FuelLogResponseDto>> GetFuelLogsForVehicleAsync(int vehicleId);
    Task<decimal> GetTotalFuelCostAsync(int vehicleId, DateTime startDate, DateTime endDate);
    Task<decimal> GetAverageFuelConsumptionAsync(int vehicleId);
    Task<FuelLogResponseDto> CreateFuelLogAsync(FuelLogCreateDto fuelLogDto);
    Task<FuelLogResponseDto?> UpdateFuelLogAsync(int id, FuelLogUpdateDto fuelLogDto);
    Task<bool> DeleteFuelLogAsync(int id);
}
