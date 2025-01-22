using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Entities;

public class Employee
{
    public int Id { get; init; }

    [MaxLength(255)]
    public required string Name { get; private set; }

    [Required]
    public required int DepartmentId { get; set; }

    public Department? Department { get; set; }
}


public record EmployeeDto(string Name, string DepartmentName);