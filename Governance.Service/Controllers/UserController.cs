using Governance.Service.Models;
using Governance.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Governance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly InMemoryRepository<User> _repo;

    public UserController(InMemoryRepository<User> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User u)
    {
        await _repo.AddAsync(u);
        return Ok(u);
    }
}