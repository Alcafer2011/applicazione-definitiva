using Governance.Service.Models;
using Governance.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Governance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly InMemoryRepository<Role> _repo;

    public RoleController(InMemoryRepository<Role> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Role r)
    {
        await _repo.AddAsync(r);
        return Ok(r);
    }
}