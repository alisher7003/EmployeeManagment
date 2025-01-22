using EmployeeManagment.Controllers;
using EmployeeManagment.Data;
using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EMTests.ControllerTests;

public class EmployeesControllerTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsOkObjectResult_WithEmployee()
    {
        // Arrange
        var employeeService = new Mock<IEmployeeService>();
        var employee = new Employee { Id = 1, Name = "John", Department = "IT" };
        employeeService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(employee);
        var controller = new EmployeesController(employeeService.Object);

        // Act
        var result = await controller.Get(1);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var employeeResult = Assert.IsType<Employee>(okObjectResult.Value);
        Assert.Equal(1, employeeResult.Id);
        Assert.Equal("John", employeeResult.Name);
        Assert.Equal("IT", employeeResult.Department);
    }
}