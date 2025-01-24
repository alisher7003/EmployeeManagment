using EmployeeManagment.Data;
using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Services;

public sealed class EmployeeService(EMDbContext dbContext) : IEmployeeService
{
    public async Task AddAsync(EmployeeDto employeeDto)
    {
        var newEmployee = new Employee { Name = employeeDto.Name, DepartmentId = employeeDto.DepartmentId };
        await dbContext.Employees.AddAsync(newEmployee);
        await dbContext.SaveChangesAsync();
    }

    public Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus)
    {
        return Task.FromResult(salary + bonus);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int Id)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Id == Id)
            .Select(e => new EmployeeDto(e.Name, e.DepartmentId))
            .FirstOrDefaultAsync();
    }
}
