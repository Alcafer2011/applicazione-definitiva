using CRM.Service.Models;
using CRM.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    private readonly InMemoryRepository<Activity> _repo;

    public ActivityController(InMemoryRepository<Activity> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Activity a)
    {
        await _repo.AddAsync(a);
        return Ok(a);
    }
}