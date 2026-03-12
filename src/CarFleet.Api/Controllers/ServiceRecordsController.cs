using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/service-records")]
[Produces("application/json")]
public class ServiceRecordsController : ControllerBase
{
    private readonly IServiceRecordService _serviceRecordService;
    private readonly ILogger<ServiceRecordsController> _logger;

    public ServiceRecordsController(IServiceRecordService serviceRecordService, ILogger<ServiceRecordsController> logger)
    {
        _serviceRecordService = serviceRecordService;
        _logger = logger;
    }

    /// <summary>
    /// Get all service records
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRecordResponseDto>>> GetAllServiceRecords()
    {
        var serviceRecords = await _serviceRecordService.GetAllServiceRecordsAsync();
        return Ok(serviceRecords);
    }

    /// <summary>
    /// Get service record by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceRecordResponseDto>> GetServiceRecordById(int id)
    {
        var serviceRecord = await _serviceRecordService.GetServiceRecordByIdAsync(id);
        if (serviceRecord == null)
            return NotFound(new { message = $"Service record with id {id} not found" });
        return Ok(serviceRecord);
    }

    /// <summary>
    /// Get service records for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRecordResponseDto>>> GetServiceRecordsForVehicle(int vehicleId)
    {
        var serviceRecords = await _serviceRecordService.GetServiceRecordsForVehicleAsync(vehicleId);
        return Ok(serviceRecords);
    }

    /// <summary>
    /// Get overdue service records
    /// </summary>
    [HttpGet("overdue")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ServiceRecordResponseDto>>> GetOverdueServiceRecords()
    {
        var serviceRecords = await _serviceRecordService.GetOverdueServiceRecordsAsync();
        return Ok(serviceRecords);
    }

    /// <summary>
    /// Get total maintenance cost for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/total-cost")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTotalMaintenanceCost(int vehicleId)
    {
        var totalCost = await _serviceRecordService.GetTotalMaintenanceCostAsync(vehicleId);
        return Ok(new { vehicleId, totalMaintenanceCost = totalCost });
    }

    /// <summary>
    /// Create a new service record
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceRecordResponseDto>> CreateServiceRecord([FromBody] ServiceRecordCreateDto serviceRecordDto)
    {
        try
        {
            var serviceRecord = await _serviceRecordService.CreateServiceRecordAsync(serviceRecordDto);
            return CreatedAtAction(nameof(GetServiceRecordById), new { id = serviceRecord.Id }, serviceRecord);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating service record");
            return BadRequest(new { message = "Error creating service record", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a service record
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceRecordResponseDto>> UpdateServiceRecord(int id, [FromBody] ServiceRecordUpdateDto serviceRecordDto)
    {
        try
        {
            var serviceRecord = await _serviceRecordService.UpdateServiceRecordAsync(id, serviceRecordDto);
            if (serviceRecord == null)
                return NotFound(new { message = $"Service record with id {id} not found" });
            return Ok(serviceRecord);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating service record");
            return BadRequest(new { message = "Error updating service record", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a service record
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServiceRecord(int id)
    {
        var result = await _serviceRecordService.DeleteServiceRecordAsync(id);
        if (!result)
            return NotFound(new { message = $"Service record with id {id} not found" });
        return NoContent();
    }
}
