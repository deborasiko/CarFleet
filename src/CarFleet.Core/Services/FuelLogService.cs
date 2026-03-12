using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class FuelLogService : IFuelLogService
{
    private readonly IFuelLogRepository _fuelLogRepository;
    private readonly IMapper _mapper;

    public FuelLogService(IFuelLogRepository fuelLogRepository, IMapper mapper)
    {
        _fuelLogRepository = fuelLogRepository;
        _mapper = mapper;
    }

    public async Task<FuelLogResponseDto?> GetFuelLogByIdAsync(int id)
    {
        var fuelLog = await _fuelLogRepository.GetByIdAsync(id);
        return fuelLog == null ? null : _mapper.Map<FuelLogResponseDto>(fuelLog);
    }

    public async Task<IEnumerable<FuelLogResponseDto>> GetAllFuelLogsAsync()
    {
        var fuelLogs = await _fuelLogRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FuelLogResponseDto>>(fuelLogs);
    }

    public async Task<IEnumerable<FuelLogResponseDto>> GetFuelLogsForVehicleAsync(int vehicleId)
    {
        var fuelLogs = await _fuelLogRepository.GetFuelLogsForVehicleAsync(vehicleId);
        return _mapper.Map<IEnumerable<FuelLogResponseDto>>(fuelLogs);
    }

    public async Task<decimal> GetTotalFuelCostAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await _fuelLogRepository.GetTotalFuelCostAsync(vehicleId, startDate, endDate);
    }

    public async Task<decimal> GetAverageFuelConsumptionAsync(int vehicleId)
    {
        return await _fuelLogRepository.GetAverageFuelConsumptionAsync(vehicleId);
    }

    public async Task<FuelLogResponseDto> CreateFuelLogAsync(FuelLogCreateDto fuelLogDto)
    {
        var fuelLog = _mapper.Map<FuelLog>(fuelLogDto);
        fuelLog.TotalCost = fuelLog.Liters * fuelLog.PricePerLiter;
        await _fuelLogRepository.AddAsync(fuelLog);
        await _fuelLogRepository.SaveChangesAsync();
        return _mapper.Map<FuelLogResponseDto>(fuelLog);
    }

    public async Task<FuelLogResponseDto?> UpdateFuelLogAsync(int id, FuelLogUpdateDto fuelLogDto)
    {
        var fuelLog = await _fuelLogRepository.GetByIdAsync(id);
        if (fuelLog == null) return null;

        _mapper.Map(fuelLogDto, fuelLog);
        if (fuelLog.Liters > 0 && fuelLog.PricePerLiter > 0)
        {
            fuelLog.TotalCost = fuelLog.Liters * fuelLog.PricePerLiter;
        }
        _fuelLogRepository.Update(fuelLog);
        await _fuelLogRepository.SaveChangesAsync();
        return _mapper.Map<FuelLogResponseDto>(fuelLog);
    }

    public async Task<bool> DeleteFuelLogAsync(int id)
    {
        var fuelLog = await _fuelLogRepository.GetByIdAsync(id);
        if (fuelLog == null) return false;

        _fuelLogRepository.Remove(fuelLog);
        await _fuelLogRepository.SaveChangesAsync();
        return true;
    }
}
