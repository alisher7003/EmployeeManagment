using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using EmployeeManagment.Services;
using EMTests.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EMTests.ServiceTests;

public class EmployeeServiceTests : IClassFixture<TestBaseSomething>
{
    private readonly IServiceProvider serviceProvider;

    public EmployeeServiceTests(TestBaseSomething testBaseSomething)
    {
        serviceProvider = testBaseSomething.serviceProvider;
    }

    [Fact]
    public async Task AddEmployeeAndGetById_ReturnsEmployee()
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
        var newEmployee = new EmployeeDto { Name = "Toshmat", DepartmentId = 1 };
        var createdEmployeeId = await employeeService.AddAsync(newEmployee);

        Assert.True(createdEmployeeId > 0, "Employee ID should be greater than zero.");
    }

    //[Theory]
    //[InlineData(100, 20, 120)]
    //[InlineData(200, 30, 230)]
    //[InlineData(300, 40, 340)]
    //[InlineData(int.MaxValue, 0, int.MaxValue)]
    //[InlineData(100.50, 20.50, 121)]
    //public async Task AddSalaryAndBonus_Succeed(decimal salary, decimal bonus, decimal expectedResult)
    //{
    //    testBaseSomething.client.GetAsync("Employees");
    //    // Arrange
    //    var employeeService = new EmployeeService();

    //    // Act
    //    var result = await employeeService.AddSalaryAndBonus(salary, bonus);

    //    // Assert
    //    Assert.Equal(expectedResult, result);
    //}

    //[Theory]
    //[InlineData(1, "John Doe", "HR")]
    //[InlineData(2, "Gishmat", "IT")]
    //[InlineData(3, "Toshmat", "IT")]
    //[InlineData(4, "Someone", "Marketing")]
    //[InlineData(5, null, null)]
    //public async Task GetById_ReturnsEmployee(int Id, string? name, string? department)
    //{
    //    var employeeService = new EmployeeService();

    //    var result = await employeeService.GetByIdAsync(Id);

    //    Assert.Equal(name, result?.Name);
    //    Assert.Equal(department, result?.Department);
    //}
}
