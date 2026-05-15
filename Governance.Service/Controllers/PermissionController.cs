using Governance.Service.Models;
using Governance.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Governance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly InMemoryRepository<Permission> _repo;

    public PermissionController(InMemoryRepository<Permission> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Permission p)
    {
        await _repo.AddAsync(p);
        return Ok(p);
    }
}