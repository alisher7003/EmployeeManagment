using EmployeeManagment.Entities;

namespace EmployeeManagment.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentDto?> GetByIdAsync(int Id);

    Task<int> AddAsync(DepartmentDto departmentDto);
}
