using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<IEnumerable<Expense>> GetExpensesForVehicleAsync(int vehicleId);
    Task<decimal> GetTotalExpensesAsync(int vehicleId, DateTime startDate, DateTime endDate);
}
