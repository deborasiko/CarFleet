using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IVehicleAssignmentService
{
    Task<VehicleAssignmentResponseDto?> GetAssignmentByIdAsync(int id);
    Task<IEnumerable<VehicleAssignmentResponseDto>> GetAllAssignmentsAsync();
    Task<IEnumerable<VehicleAssignmentResponseDto>> GetActiveAssignmentsAsync();
    Task<VehicleAssignmentResponseDto?> GetActiveAssignmentForVehicleAsync(int vehicleId);
    Task<IEnumerable<VehicleAssignmentResponseDto>> GetAssignmentHistoryAsync(int vehicleId);
    Task<VehicleAssignmentResponseDto> CreateAssignmentAsync(VehicleAssignmentCreateDto assignmentDto);
    Task<VehicleAssignmentResponseDto?> UpdateAssignmentAsync(int id, VehicleAssignmentUpdateDto assignmentDto);
    Task<bool> DeleteAssignmentAsync(int id);
    Task<VehicleAssignmentResponseDto?> EndAssignmentAsync(int id);
}
