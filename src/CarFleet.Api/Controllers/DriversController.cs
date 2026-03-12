using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/drivers")]
[Produces("application/json")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;
    private readonly ILogger<DriversController> _logger;

    public DriversController(IDriverService driverService, ILogger<DriversController> logger)
    {
        _driverService = driverService;
        _logger = logger;
    }

    /// <summary>
    /// Get all drivers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetAllDrivers()
    {
        var drivers = await _driverService.GetAllDriversAsync();
        return Ok(drivers);
    }

    /// <summary>
    /// Get driver by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverResponseDto>> GetDriverById(int id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        if (driver == null)
            return NotFound(new { message = $"Driver with id {id} not found" });
        return Ok(driver);
    }

    /// <summary>
    /// Search drivers
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DriverResponseDto>>> SearchDrivers(
        [FromQuery] string? searchTerm,
        [FromQuery] DriverStatus? status)
    {
        var drivers = await _driverService.SearchDriversAsync(searchTerm, status);
        return Ok(drivers);
    }

    /// <summary>
    /// Get drivers with expired licenses
    /// </summary>
    [HttpGet("expired-licenses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetExpiredLicenseDrivers()
    {
        var drivers = await _driverService.GetExpiredLicenseDriversAsync();
        return Ok(drivers);
    }

    /// <summary>
    /// Create a new driver
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DriverResponseDto>> CreateDriver([FromBody] DriverCreateDto driverDto)
    {
        try
        {
            var driver = await _driverService.CreateDriverAsync(driverDto);
            return CreatedAtAction(nameof(GetDriverById), new { id = driver.Id }, driver);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating driver");
            return BadRequest(new { message = "Error creating driver", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a driver
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DriverResponseDto>> UpdateDriver(int id, [FromBody] DriverUpdateDto driverDto)
    {
        try
        {
            var driver = await _driverService.UpdateDriverAsync(id, driverDto);
            if (driver == null)
                return NotFound(new { message = $"Driver with id {id} not found" });
            return Ok(driver);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating driver");
            return BadRequest(new { message = "Error updating driver", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a driver
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        var result = await _driverService.DeleteDriverAsync(id);
        if (!result)
            return NotFound(new { message = $"Driver with id {id} not found" });
        return NoContent();
    }
}
