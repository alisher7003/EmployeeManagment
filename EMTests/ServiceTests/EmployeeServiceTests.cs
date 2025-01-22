using EmployeeManagment.Entities;
using EmployeeManagment.Services;

namespace EMTests.ServiceTests;

public class EmployeeServiceTests
{
    [Theory]
    [InlineData(100, 20, 120)]
    [InlineData(200, 30, 230)]
    [InlineData(300, 40, 340)]
    [InlineData(int.MaxValue, 0, int.MaxValue)]
    [InlineData(100.50, 20.50, 121)]
    public async Task AddSalaryAndBonus_Succeed(decimal salary, decimal bonus, decimal expectedResult)
    {
        // Arrange
        var employeeService = new EmployeeService();

        // Act
        var result = await employeeService.AddSalaryAndBonus(salary, bonus);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(1, "John Doe", "HR")]
    [InlineData(2, "Gishmat", "IT")]
    [InlineData(3, "Toshmat", "IT")]
    [InlineData(4, "Someone", "Marketing")]
    [InlineData(5, null, null)]
    public async Task GetById_ReturnsEmployee(int Id, string? name, string? department)
    {
        var employeeService = new EmployeeService();

        var result = await employeeService.GetByIdAsync(Id);

        Assert.Equal(name, result?.Name);
        Assert.Equal(department, result?.Department);
    }
}
