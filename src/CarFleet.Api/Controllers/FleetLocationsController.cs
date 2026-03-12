using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/fleet-locations")]
[Produces("application/json")]
public class FleetLocationsController : ControllerBase
{
    private readonly IFleetLocationService _locationService;
    private readonly ILogger<FleetLocationsController> _logger;

    public FleetLocationsController(IFleetLocationService locationService, ILogger<FleetLocationsController> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    /// <summary>
    /// Get all locations
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FleetLocationResponseDto>>> GetAllLocations()
    {
        var locations = await _locationService.GetAllLocationsAsync();
        return Ok(locations);
    }

    /// <summary>
    /// Get location by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FleetLocationResponseDto>> GetLocationById(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
            return NotFound(new { message = $"Location with id {id} not found" });
        return Ok(location);
    }

    /// <summary>
    /// Get all active locations
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FleetLocationResponseDto>>> GetActiveLocations()
    {
        var locations = await _locationService.GetActiveLocationsAsync();
        return Ok(locations);
    }

    /// <summary>
    /// Create a new location
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FleetLocationResponseDto>> CreateLocation([FromBody] FleetLocationCreateDto locationDto)
    {
        try
        {
            var location = await _locationService.CreateLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating location");
            return BadRequest(new { message = "Error creating location", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a location
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FleetLocationResponseDto>> UpdateLocation(int id, [FromBody] FleetLocationUpdateDto locationDto)
    {
        try
        {
            var location = await _locationService.UpdateLocationAsync(id, locationDto);
            if (location == null)
                return NotFound(new { message = $"Location with id {id} not found" });
            return Ok(location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating location");
            return BadRequest(new { message = "Error updating location", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a location
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var result = await _locationService.DeleteLocationAsync(id);
        if (!result)
            return NotFound(new { message = $"Location with id {id} not found" });
        return NoContent();
    }
}
