using EmployeeManagment.Data;
using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;

namespace EmployeeManagment.Services;

public sealed class EmployeeService(EMDbContext dbContext) : IEmployeeService
{
    public Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus)
    {
        return Task.FromResult(salary + bonus);
    }

    public async Task<Employee?> GetByIdAsync(int Id)
    {
        return await dbContext.FindAsync<Employee>(Id);
    }
}
