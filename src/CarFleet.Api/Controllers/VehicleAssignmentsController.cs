using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/vehicle-assignments")]
[Produces("application/json")]
public class VehicleAssignmentsController : ControllerBase
{
    private readonly IVehicleAssignmentService _assignmentService;
    private readonly ILogger<VehicleAssignmentsController> _logger;

    public VehicleAssignmentsController(IVehicleAssignmentService assignmentService, ILogger<VehicleAssignmentsController> logger)
    {
        _assignmentService = assignmentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all assignments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleAssignmentResponseDto>>> GetAllAssignments()
    {
        var assignments = await _assignmentService.GetAllAssignmentsAsync();
        return Ok(assignments);
    }

    /// <summary>
    /// Get assignment by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleAssignmentResponseDto>> GetAssignmentById(int id)
    {
        var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
        if (assignment == null)
            return NotFound(new { message = $"Assignment with id {id} not found" });
        return Ok(assignment);
    }

    /// <summary>
    /// Get all active assignments
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleAssignmentResponseDto>>> GetActiveAssignments()
    {
        var assignments = await _assignmentService.GetActiveAssignmentsAsync();
        return Ok(assignments);
    }

    /// <summary>
    /// Get active assignment for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleAssignmentResponseDto>> GetActiveAssignmentForVehicle(int vehicleId)
    {
        var assignment = await _assignmentService.GetActiveAssignmentForVehicleAsync(vehicleId);
        if (assignment == null)
            return NotFound(new { message = $"No active assignment found for vehicle {vehicleId}" });
        return Ok(assignment);
    }

    /// <summary>
    /// Get assignment history for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleAssignmentResponseDto>>> GetAssignmentHistory(int vehicleId)
    {
        var assignments = await _assignmentService.GetAssignmentHistoryAsync(vehicleId);
        return Ok(assignments);
    }

    /// <summary>
    /// Create a new assignment
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleAssignmentResponseDto>> CreateAssignment([FromBody] VehicleAssignmentCreateDto assignmentDto)
    {
        try
        {
            var assignment = await _assignmentService.CreateAssignmentAsync(assignmentDto);
            return CreatedAtAction(nameof(GetAssignmentById), new { id = assignment.Id }, assignment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating assignment");
            return BadRequest(new { message = "Error creating assignment", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an assignment
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleAssignmentResponseDto>> UpdateAssignment(int id, [FromBody] VehicleAssignmentUpdateDto assignmentDto)
    {
        try
        {
            var assignment = await _assignmentService.UpdateAssignmentAsync(id, assignmentDto);
            if (assignment == null)
                return NotFound(new { message = $"Assignment with id {id} not found" });
            return Ok(assignment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating assignment");
            return BadRequest(new { message = "Error updating assignment", error = ex.Message });
        }
    }

    /// <summary>
    /// End an assignment
    /// </summary>
    [HttpPost("{id}/end")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleAssignmentResponseDto>> EndAssignment(int id)
    {
        var assignment = await _assignmentService.EndAssignmentAsync(id);
        if (assignment == null)
            return NotFound(new { message = $"Assignment with id {id} not found" });
        return Ok(assignment);
    }

    /// <summary>
    /// Delete an assignment
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var result = await _assignmentService.DeleteAssignmentAsync(id);
        if (!result)
            return NotFound(new { message = $"Assignment with id {id} not found" });
        return NoContent();
    }
}
