using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{id}", Name = "GetEmployee")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync(EmployeeDto employeeDto)
    {
        await _employeeService.AddAsync(employeeDto);
        return Ok();
    }

    [HttpPost(Name = "AddSalaryAndBonus")]
    public Task<decimal> Post(decimal salary, decimal bonus)
    {
        return _employeeService.AddSalaryAndBonus(salary, bonus);
    }
}
