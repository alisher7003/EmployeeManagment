using EmployeeManagment.Entities;

namespace EmployeeManagment.Interfaces;

public interface IEmployeeService
{
    Task<Department?> GetByIdAsync(int Id);

    Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus);
}
