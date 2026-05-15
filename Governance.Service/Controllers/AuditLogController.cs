using Governance.Service.Models;
using Governance.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Governance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogController : ControllerBase
{
    private readonly InMemoryRepository<AuditLog> _repo;

    public AuditLogController(InMemoryRepository<AuditLog> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuditLog log)
    {
        await _repo.AddAsync(log);
        return Ok(log);
    }
}