using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/expenses")]
[Produces("application/json")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<ExpensesController> _logger;

    public ExpensesController(IExpenseService expenseService, ILogger<ExpensesController> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    /// <summary>
    /// Get all expenses
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseResponseDto>>> GetAllExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

    /// <summary>
    /// Get expense by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseResponseDto>> GetExpenseById(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
            return NotFound(new { message = $"Expense with id {id} not found" });
        return Ok(expense);
    }

    /// <summary>
    /// Get expenses for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseResponseDto>>> GetExpensesForVehicle(int vehicleId)
    {
        var expenses = await _expenseService.GetExpensesForVehicleAsync(vehicleId);
        return Ok(expenses);
    }

    /// <summary>
    /// Get total expenses for a vehicle in a date range
    /// </summary>
    [HttpGet("vehicle/{vehicleId}/total")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTotalExpenses(int vehicleId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var totalExpenses = await _expenseService.GetTotalExpensesAsync(vehicleId, startDate, endDate);
        return Ok(new { vehicleId, startDate, endDate, totalExpenses });
    }

    /// <summary>
    /// Create a new expense
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseResponseDto>> CreateExpense([FromBody] ExpenseCreateDto expenseDto)
    {
        try
        {
            var expense = await _expenseService.CreateExpenseAsync(expenseDto);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating expense");
            return BadRequest(new { message = "Error creating expense", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an expense
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseResponseDto>> UpdateExpense(int id, [FromBody] ExpenseUpdateDto expenseDto)
    {
        try
        {
            var expense = await _expenseService.UpdateExpenseAsync(id, expenseDto);
            if (expense == null)
                return NotFound(new { message = $"Expense with id {id} not found" });
            return Ok(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating expense");
            return BadRequest(new { message = "Error updating expense", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete an expense
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var result = await _expenseService.DeleteExpenseAsync(id);
        if (!result)
            return NotFound(new { message = $"Expense with id {id} not found" });
        return NoContent();
    }
}
