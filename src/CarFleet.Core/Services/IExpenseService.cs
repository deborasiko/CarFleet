using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IExpenseService
{
    Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id);
    Task<IEnumerable<ExpenseResponseDto>> GetAllExpensesAsync();
    Task<IEnumerable<ExpenseResponseDto>> GetExpensesForVehicleAsync(int vehicleId);
    Task<decimal> GetTotalExpensesAsync(int vehicleId, DateTime startDate, DateTime endDate);
    Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto);
    Task<ExpenseResponseDto?> UpdateExpenseAsync(int id, ExpenseUpdateDto expenseDto);
    Task<bool> DeleteExpenseAsync(int id);
}
