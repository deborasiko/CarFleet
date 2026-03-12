using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/vehicles")]
[Produces("application/json")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger)
    {
        _vehicleService = vehicleService;
        _logger = logger;
    }

    /// <summary>
    /// Get all vehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAllVehicles()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    /// <summary>
    /// Get vehicle by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleResponseDto>> GetVehicleById(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Vehicle with id {id} not found" });
        return Ok(vehicle);
    }

    /// <summary>
    /// Search vehicles
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> SearchVehicles(
        [FromQuery] string? searchTerm,
        [FromQuery] int? year,
        [FromQuery] string? make,
        [FromQuery] VehicleStatus? status)
    {
        var vehicles = await _vehicleService.SearchVehiclesAsync(searchTerm, year, make, status);
        return Ok(vehicles);
    }

    /// <summary>
    /// Create a new vehicle
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleResponseDto>> CreateVehicle([FromBody] VehicleCreateDto vehicleDto)
    {
        try
        {
            var vehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, vehicle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating vehicle");
            return BadRequest(new { message = "Error creating vehicle", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a vehicle
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleResponseDto>> UpdateVehicle(int id, [FromBody] VehicleUpdateDto vehicleDto)
    {
        try
        {
            var vehicle = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
            if (vehicle == null)
                return NotFound(new { message = $"Vehicle with id {id} not found" });
            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vehicle");
            return BadRequest(new { message = "Error updating vehicle", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a vehicle
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var result = await _vehicleService.DeleteVehicleAsync(id);
        if (!result)
            return NotFound(new { message = $"Vehicle with id {id} not found" });
        return NoContent();
    }
}
