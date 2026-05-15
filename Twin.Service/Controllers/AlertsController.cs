using Microsoft.AspNetCore.Mvc;
using Twin.Service.Models;
using Twin.Service.Repositories;

namespace Twin.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly InMemoryRepository<SystemAlert> _alerts;

    public AlertsController(InMemoryRepository<SystemAlert> alerts)
    {
        _alerts = alerts;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _alerts.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SystemAlert alert)
    {
        await _alerts.AddAsync(alert);
        return Ok(alert);
    }
}