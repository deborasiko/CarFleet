using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/fuel-logs")]
[Produces("application/json")]
public class FuelLogsController : ControllerBase
{
    private readonly IFuelLogService _fuelLogService;
    private readonly ILogger<FuelLogsController> _logger;

    public FuelLogsController(IFuelLogService fuelLogService, ILogger<FuelLogsController> logger)
    {
        _fuelLogService = fuelLogService;
        _logger = logger;
    }

    /// <summary>
    /// Get all fuel logs
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FuelLogResponseDto>>> GetAllFuelLogs()
    {
        var fuelLogs = await _fuelLogService.GetAllFuelLogsAsync();
        return Ok(fuelLogs);
    }

    /// <summary>
    /// Get fuel log by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FuelLogResponseDto>> GetFuelLogById(int id)
    {
        var fuelLog = await _fuelLogService.GetFuelLogByIdAsync(id);
        if (fuelLog == null)
            return NotFound(new { message = $"Fuel log with id {id} not found" });
        return Ok(fuelLog);
    }

    /// <summary>
    /// Get fuel logs for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FuelLogResponseDto>>> GetFuelLogsForVehicle(int vehicleId)
    {
        var fuelLogs = await _fuelLogService.GetFuelLogsForVehicleAsync(vehicleId);
        return Ok(fuelLogs);
    }

    /// <summary>
    /// Get total fuel cost for a vehicle in a date range
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/cost")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTotalFuelCost(int vehicleId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var totalCost = await _fuelLogService.GetTotalFuelCostAsync(vehicleId, startDate, endDate);
        return Ok(new { vehicleId, startDate, endDate, totalCost });
    }

    /// <summary>
    /// Get average fuel consumption for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/average-consumption")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAverageFuelConsumption(int vehicleId)
    {
        var avgConsumption = await _fuelLogService.GetAverageFuelConsumptionAsync(vehicleId);
        return Ok(new { vehicleId, averageConsumptionKmPerLiter = avgConsumption });
    }

    /// <summary>
    /// Create a new fuel log
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FuelLogResponseDto>> CreateFuelLog([FromBody] FuelLogCreateDto fuelLogDto)
    {
        try
        {
            var fuelLog = await _fuelLogService.CreateFuelLogAsync(fuelLogDto);
            return CreatedAtAction(nameof(GetFuelLogById), new { id = fuelLog.Id }, fuelLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating fuel log");
            return BadRequest(new { message = "Error creating fuel log", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a fuel log
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FuelLogResponseDto>> UpdateFuelLog(int id, [FromBody] FuelLogUpdateDto fuelLogDto)
    {
        try
        {
            var fuelLog = await _fuelLogService.UpdateFuelLogAsync(id, fuelLogDto);
            if (fuelLog == null)
                return NotFound(new { message = $"Fuel log with id {id} not found" });
            return Ok(fuelLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating fuel log");
            return BadRequest(new { message = "Error updating fuel log", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a fuel log
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFuelLog(int id)
    {
        var result = await _fuelLogService.DeleteFuelLogAsync(id);
        if (!result)
            return NotFound(new { message = $"Fuel log with id {id} not found" });
        return NoContent();
    }
}
