using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        return expense == null ? null : _mapper.Map<ExpenseResponseDto>(expense);
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllExpensesAsync()
    {
        var expenses = await _expenseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExpenseResponseDto>>(expenses);
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesForVehicleAsync(int vehicleId)
    {
        var expenses = await _expenseRepository.GetExpensesForVehicleAsync(vehicleId);
        return _mapper.Map<IEnumerable<ExpenseResponseDto>>(expenses);
    }

    public async Task<decimal> GetTotalExpensesAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await _expenseRepository.GetTotalExpensesAsync(vehicleId, startDate, endDate);
    }

    public async Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto)
    {
        var expense = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.AddAsync(expense);
        await _expenseRepository.SaveChangesAsync();
        return _mapper.Map<ExpenseResponseDto>(expense);
    }

    public async Task<ExpenseResponseDto?> UpdateExpenseAsync(int id, ExpenseUpdateDto expenseDto)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return null;

        _mapper.Map(expenseDto, expense);
        _expenseRepository.Update(expense);
        await _expenseRepository.SaveChangesAsync();
        return _mapper.Map<ExpenseResponseDto>(expense);
    }

    public async Task<bool> DeleteExpenseAsync(int id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return false;

        _expenseRepository.Remove(expense);
        await _expenseRepository.SaveChangesAsync();
        return true;
    }
}
