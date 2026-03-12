using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class ExpenseRepository : Repository<Expense>, IExpenseRepository
{
    public ExpenseRepository(FleetDbContext context) : base(context) { }

    public async Task<IEnumerable<Expense>> GetExpensesForVehicleAsync(int vehicleId)
    {
        return await DbSet
            .Where(e => e.VehicleId == vehicleId)
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalExpensesAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await DbSet
            .Where(e => e.VehicleId == vehicleId && e.ExpenseDate >= startDate && e.ExpenseDate <= endDate)
            .SumAsync(e => e.Amount);
    }
}
