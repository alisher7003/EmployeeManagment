using EmployeeManagment.Entities;

namespace EmployeeManagment.Interfaces;

public interface IEmployeeService
{
    Task<Employee?> GetByIdAsync(int Id);

    Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus);
}
