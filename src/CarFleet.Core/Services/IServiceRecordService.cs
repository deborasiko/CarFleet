using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IServiceRecordService
{
    Task<ServiceRecordResponseDto?> GetServiceRecordByIdAsync(int id);
    Task<IEnumerable<ServiceRecordResponseDto>> GetAllServiceRecordsAsync();
    Task<IEnumerable<ServiceRecordResponseDto>> GetServiceRecordsForVehicleAsync(int vehicleId);
    Task<IEnumerable<ServiceRecordResponseDto>> GetOverdueServiceRecordsAsync();
    Task<decimal> GetTotalMaintenanceCostAsync(int vehicleId);
    Task<ServiceRecordResponseDto> CreateServiceRecordAsync(ServiceRecordCreateDto serviceRecordDto);
    Task<ServiceRecordResponseDto?> UpdateServiceRecordAsync(int id, ServiceRecordUpdateDto serviceRecordDto);
    Task<bool> DeleteServiceRecordAsync(int id);
}
