using EmployeeManagment.Data;
using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Services;

public sealed class EmployeeService(EMDbContext dbContext) : IEmployeeService
{
    public Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus)
    {
        return Task.FromResult(salary + bonus);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int Id)
    {
        return await dbContext.Employees
            .Where(e => e.Id == Id)
            .Select(e => new EmployeeDto(e.Name, e))
            .FirstOrDefaultAsync();
    }
}
