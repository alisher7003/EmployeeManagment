using EmployeeManagment.Entities;
using EmployeeManagment.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        this.departmentService = departmentService;
    }

    [HttpGet("{id}", Name = "GetDepartment")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await departmentService.GetByIdAsync(id));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(DepartmentDto department)
    {
        return Ok(await departmentService.AddAsync(department));
    }
}
