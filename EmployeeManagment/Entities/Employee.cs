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

public class EmployeeDetails
{
    public string SecretKeyForPayment
    {
        get; set;
    }

    public int DepartmentKey
    {
        get; set;

    }
}


    public record EmployeeDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public int DepartmentId { get; set; }
}

public class User
{
    public int Id { get; set; }
    public ICollection<UserGroup> Groups { get; set; }
}

public class Group
{
    public int Id { get; set; }
    public ICollection<UserGroup> Users { get; set; }
}

public class UserGroup
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
}