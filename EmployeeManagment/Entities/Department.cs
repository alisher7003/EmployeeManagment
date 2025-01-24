using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Entities;

public class Department
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}

public record DepartmentDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }
}