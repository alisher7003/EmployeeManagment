using EmployeeManagment.Data;
using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Services;

public class DepartmentService(EMDbContext dbContext) : IDepartmentService
{
    public async Task<int> AddAsync(DepartmentDto departmentDto)
    {
        var newDepartment = new Department { Name = departmentDto.Name };

        await dbContext.Departments.AddAsync(newDepartment);
        await dbContext.SaveChangesAsync();

        return newDepartment.Id;
    }

    public Task<DepartmentDto?> GetByIdAsync(int Id)
    {
        return dbContext.Departments
            .AsNoTracking()
            .Where(d => d.Id == Id)
            .Select(d => new DepartmentDto { Id = d.Id, Name = d.Name })
            .FirstOrDefaultAsync();
    }
}
