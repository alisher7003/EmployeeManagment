using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;

namespace EmployeeManagment.Services;

public sealed class EmployeeService : IEmployeeService
{
    private List<Employee> _employees = new List<Employee>()
    {
        new Employee
        {
            Id = 1,
            Name = "John Doe",
            Department = "HR"
        },
        new Employee
        {
            Id = 2,
            Name = "Gishmat",
            Department = "IT"
        },
        new Employee
        {
            Id = 3,
            Name = "Toshmat",
            Department = "IT"
        },
        new Employee
        {
            Id = 4,
            Name = "Someone",
            Department = "Marketing"
        }
    };

    public Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus)
    {
        return Task.FromResult(salary + bonus);
    }

    public Task<Employee?> GetByIdAsync(int Id)
    {
        return Task.FromResult(_employees.FirstOrDefault(e => e.Id == Id));
    }
}
