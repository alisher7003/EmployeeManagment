using EmployeeManagment.Entities;

namespace EmployeeManagment.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDto?> GetByIdAsync(int Id);

    Task<int> AddAsync(EmployeeDto employeeDto);

    Task<decimal> AddSalaryAndBonus(decimal salary, decimal bonus);
}
