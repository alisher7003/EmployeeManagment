using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Entities;

public class Employee
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    public required int DepartmentId { get; set; }

    public Department? Department { get; set; }
}