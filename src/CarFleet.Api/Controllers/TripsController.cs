using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/trips")]
[Produces("application/json")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly ILogger<TripsController> _logger;

    public TripsController(ITripService tripService, ILogger<TripsController> logger)
    {
        _tripService = tripService;
        _logger = logger;
    }

    /// <summary>
    /// Get all trips
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetAllTrips()
    {
        var trips = await _tripService.GetAllTripsAsync();
        return Ok(trips);
    }

    /// <summary>
    /// Get trip by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripResponseDto>> GetTripById(int id)
    {
        var trip = await _tripService.GetTripByIdAsync(id);
        if (trip == null)
            return NotFound(new { message = $"Trip with id {id} not found" });
        return Ok(trip);
    }

    /// <summary>
    /// Get trips for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsForVehicle(int vehicleId)
    {
        var trips = await _tripService.GetTripsForVehicleAsync(vehicleId);
        return Ok(trips);
    }

    /// <summary>
    /// Get trips for a driver
    /// </summary>
    [HttpGet("driver/{driverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsForDriver(int driverId)
    {
        var trips = await _tripService.GetTripsForDriverAsync(driverId);
        return Ok(trips);
    }

    /// <summary>
    /// Get total distance for a vehicle in a date range
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/distance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTotalDistance(int vehicleId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var totalDistance = await _tripService.GetTotalDistanceAsync(vehicleId, startDate, endDate);
        return Ok(new { vehicleId, startDate, endDate, totalDistanceKm = totalDistance });
    }

    /// <summary>
    /// Create a new trip
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TripResponseDto>> CreateTrip([FromBody] TripCreateDto tripDto)
    {
        try
        {
            var trip = await _tripService.CreateTripAsync(tripDto);
            return CreatedAtAction(nameof(GetTripById), new { id = trip.Id }, trip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating trip");
            return BadRequest(new { message = "Error creating trip", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a trip
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TripResponseDto>> UpdateTrip(int id, [FromBody] TripUpdateDto tripDto)
    {
        try
        {
            var trip = await _tripService.UpdateTripAsync(id, tripDto);
            if (trip == null)
                return NotFound(new { message = $"Trip with id {id} not found" });
            return Ok(trip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating trip");
            return BadRequest(new { message = "Error updating trip", error = ex.Message });
        }
    }

    /// <summary>
    /// End a trip
    /// </summary>
    [HttpPost("{id}/end")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripResponseDto>> EndTrip(int id)
    {
        var trip = await _tripService.EndTripAsync(id);
        if (trip == null)
            return NotFound(new { message = $"Trip with id {id} not found" });
        return Ok(trip);
    }

    /// <summary>
    /// Delete a trip
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var result = await _tripService.DeleteTripAsync(id);
        if (!result)
            return NotFound(new { message = $"Trip with id {id} not found" });
        return NoContent();
    }
}
