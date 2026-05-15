using Microsoft.AspNetCore.Mvc;
using Twin.Service.Models;
using Twin.Service.Repositories;

namespace Twin.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetricsController : ControllerBase
{
    private readonly InMemoryRepository<SystemMetric> _metrics;

    public MetricsController(InMemoryRepository<SystemMetric> metrics)
    {
        _metrics = metrics;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _metrics.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SystemMetric metric)
    {
        await _metrics.AddAsync(metric);
        return Ok(metric);
    }
}