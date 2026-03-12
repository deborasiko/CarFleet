using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class ServiceRecordService : IServiceRecordService
{
    private readonly IServiceRecordRepository _serviceRecordRepository;
    private readonly IMapper _mapper;

    public ServiceRecordService(IServiceRecordRepository serviceRecordRepository, IMapper mapper)
    {
        _serviceRecordRepository = serviceRecordRepository;
        _mapper = mapper;
    }

    public async Task<ServiceRecordResponseDto?> GetServiceRecordByIdAsync(int id)
    {
        var serviceRecord = await _serviceRecordRepository.GetByIdAsync(id);
        return serviceRecord == null ? null : _mapper.Map<ServiceRecordResponseDto>(serviceRecord);
    }

    public async Task<IEnumerable<ServiceRecordResponseDto>> GetAllServiceRecordsAsync()
    {
        var serviceRecords = await _serviceRecordRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceRecordResponseDto>>(serviceRecords);
    }

    public async Task<IEnumerable<ServiceRecordResponseDto>> GetServiceRecordsForVehicleAsync(int vehicleId)
    {
        var serviceRecords = await _serviceRecordRepository.GetServiceRecordsForVehicleAsync(vehicleId);
        return _mapper.Map<IEnumerable<ServiceRecordResponseDto>>(serviceRecords);
    }

    public async Task<IEnumerable<ServiceRecordResponseDto>> GetOverdueServiceRecordsAsync()
    {
        var serviceRecords = await _serviceRecordRepository.GetOverdueServiceRecordsAsync();
        return _mapper.Map<IEnumerable<ServiceRecordResponseDto>>(serviceRecords);
    }

    public async Task<decimal> GetTotalMaintenanceCostAsync(int vehicleId)
    {
        return await _serviceRecordRepository.GetTotalMaintenanceCostAsync(vehicleId);
    }

    public async Task<ServiceRecordResponseDto> CreateServiceRecordAsync(ServiceRecordCreateDto serviceRecordDto)
    {
        var serviceRecord = _mapper.Map<ServiceRecord>(serviceRecordDto);
        await _serviceRecordRepository.AddAsync(serviceRecord);
        await _serviceRecordRepository.SaveChangesAsync();
        return _mapper.Map<ServiceRecordResponseDto>(serviceRecord);
    }

    public async Task<ServiceRecordResponseDto?> UpdateServiceRecordAsync(int id, ServiceRecordUpdateDto serviceRecordDto)
    {
        var serviceRecord = await _serviceRecordRepository.GetByIdAsync(id);
        if (serviceRecord == null) return null;

        _mapper.Map(serviceRecordDto, serviceRecord);
        _serviceRecordRepository.Update(serviceRecord);
        await _serviceRecordRepository.SaveChangesAsync();
        return _mapper.Map<ServiceRecordResponseDto>(serviceRecord);
    }

    public async Task<bool> DeleteServiceRecordAsync(int id)
    {
        var serviceRecord = await _serviceRecordRepository.GetByIdAsync(id);
        if (serviceRecord == null) return false;

        _serviceRecordRepository.Remove(serviceRecord);
        await _serviceRecordRepository.SaveChangesAsync();
        return true;
    }
}
