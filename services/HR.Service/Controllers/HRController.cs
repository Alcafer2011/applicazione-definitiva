using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.HR.Service.Models;

namespace AIEnterpriseOS.HR.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HRController : ControllerBase
{
    private static readonly List<Employee> _employees = new();
    private static readonly List<HrEvent> _events = new();

    [HttpPost("employees")]
    public IActionResult UpsertEmployee([FromBody] Employee employee)
    {
        var existing = _employees.FirstOrDefault(e => e.Id == employee.Id);
        if (existing is null)
        {
            employee.Id = string.IsNullOrWhiteSpace(employee.Id) ? Guid.NewGuid().ToString() : employee.Id;
            _employees.Add(employee);
        }
        else
        {
            existing.FullName = employee.FullName;
            existing.Role = employee.Role;
            existing.External = employee.External;
        }

        return Ok(employee);
    }

    [HttpGet("employees")]
    public IActionResult GetEmployees() => Ok(_employees);

    [HttpPost("events")]
    public IActionResult AddEvent([FromBody] HrEvent ev)
    {
        ev.Id = string.IsNullOrWhiteSpace(ev.Id) ? Guid.NewGuid().ToString() : ev.Id;
        _events.Add(ev);
        return Ok(ev);
    }

    [HttpGet("events")]
    public IActionResult GetEvents([FromQuery] string? employeeId = null)
    {
        var query = _events.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(employeeId))
            query = query.Where(e => e.EmployeeId == employeeId);

        return Ok(query.ToList());
    }
}
