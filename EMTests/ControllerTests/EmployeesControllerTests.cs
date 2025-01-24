using EmployeeManagment.Entities;
using EMTests.Abstractions;
using System.Net.Http.Json;

namespace EMTests.ControllerTests;

public class EmployeesControllerTests : IClassFixture<TestBaseSomething>
{
    private readonly HttpClient _client;

    public EmployeesControllerTests(TestBaseSomething testBaseSomething)
    {
        _client = testBaseSomething.client;
    }

    [Fact]
    public async Task PostEmployeeAndGetById_ReturnsEmployee()
    {
        var newEmployee = new EmployeeDto { Name = "Toshmat", DepartmentId = 1 };
        var postEmployeeResponse = await _client.PostAsJsonAsync("/api/employees/create", newEmployee);
        postEmployeeResponse.EnsureSuccessStatusCode();

        var createdEmployeeId = int.Parse(await postEmployeeResponse.Content.ReadAsStringAsync());
        Assert.True(createdEmployeeId > 0, "Employee ID should be greater than zero.");

        var getResponse = await _client.GetAsync($"/api/employees/{createdEmployeeId}");
        getResponse.EnsureSuccessStatusCode();

        var fetchedEmployee = await getResponse.Content.ReadFromJsonAsync<EmployeeDto>();

        Assert.NotNull(fetchedEmployee);
        Assert.Equal(newEmployee.Name, fetchedEmployee.Name);
        Assert.Equal(newEmployee.DepartmentId, fetchedEmployee.DepartmentId);
    }
}