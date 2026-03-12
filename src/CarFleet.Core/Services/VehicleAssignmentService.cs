using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class VehicleAssignmentService : IVehicleAssignmentService
{
    private readonly IVehicleAssignmentRepository _assignmentRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public VehicleAssignmentService(
        IVehicleAssignmentRepository assignmentRepository,
        IVehicleRepository vehicleRepository,
        IDriverRepository driverRepository,
        IMapper mapper)
    {
        _assignmentRepository = assignmentRepository;
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task<VehicleAssignmentResponseDto?> GetAssignmentByIdAsync(int id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id);
        return assignment == null ? null : _mapper.Map<VehicleAssignmentResponseDto>(assignment);
    }

    public async Task<IEnumerable<VehicleAssignmentResponseDto>> GetAllAssignmentsAsync()
    {
        var assignments = await _assignmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VehicleAssignmentResponseDto>>(assignments);
    }

    public async Task<IEnumerable<VehicleAssignmentResponseDto>> GetActiveAssignmentsAsync()
    {
        var assignments = await _assignmentRepository.GetActiveAssignmentsAsync();
        return _mapper.Map<IEnumerable<VehicleAssignmentResponseDto>>(assignments);
    }

    public async Task<VehicleAssignmentResponseDto?> GetActiveAssignmentForVehicleAsync(int vehicleId)
    {
        var assignment = await _assignmentRepository.GetActiveAssignmentForVehicleAsync(vehicleId);
        return assignment == null ? null : _mapper.Map<VehicleAssignmentResponseDto>(assignment);
    }

    public async Task<IEnumerable<VehicleAssignmentResponseDto>> GetAssignmentHistoryAsync(int vehicleId)
    {
        var assignments = await _assignmentRepository.GetAssignmentHistoryAsync(vehicleId);
        return _mapper.Map<IEnumerable<VehicleAssignmentResponseDto>>(assignments);
    }

    public async Task<VehicleAssignmentResponseDto> CreateAssignmentAsync(VehicleAssignmentCreateDto assignmentDto)
    {
        // Validate vehicle exists
        var vehicle = await _vehicleRepository.GetByIdAsync(assignmentDto.VehicleId);
        if (vehicle == null)
            throw new InvalidOperationException($"Vehicle with id {assignmentDto.VehicleId} not found");

        // Validate driver exists
        var driver = await _driverRepository.GetByIdAsync(assignmentDto.DriverId);
        if (driver == null)
            throw new InvalidOperationException($"Driver with id {assignmentDto.DriverId} not found");

        // Check for existing active assignments
        var existingAssignment = await _assignmentRepository.GetActiveAssignmentForVehicleAsync(assignmentDto.VehicleId);
        if (existingAssignment != null)
            throw new InvalidOperationException("This vehicle already has an active assignment");

        var assignment = _mapper.Map<VehicleAssignment>(assignmentDto);
        await _assignmentRepository.AddAsync(assignment);
        await _assignmentRepository.SaveChangesAsync();
        return _mapper.Map<VehicleAssignmentResponseDto>(assignment);
    }

    public async Task<VehicleAssignmentResponseDto?> UpdateAssignmentAsync(int id, VehicleAssignmentUpdateDto assignmentDto)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id);
        if (assignment == null) return null;

        _mapper.Map(assignmentDto, assignment);
        _assignmentRepository.Update(assignment);
        await _assignmentRepository.SaveChangesAsync();
        return _mapper.Map<VehicleAssignmentResponseDto>(assignment);
    }

    public async Task<bool> DeleteAssignmentAsync(int id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id);
        if (assignment == null) return false;

        _assignmentRepository.Remove(assignment);
        await _assignmentRepository.SaveChangesAsync();
        return true;
    }

    public async Task<VehicleAssignmentResponseDto?> EndAssignmentAsync(int id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id);
        if (assignment == null) return null;

        assignment.EndDate = DateTime.Now;
        _assignmentRepository.Update(assignment);
        await _assignmentRepository.SaveChangesAsync();
        return _mapper.Map<VehicleAssignmentResponseDto>(assignment);
    }
}
