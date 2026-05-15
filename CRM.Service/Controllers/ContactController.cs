using CRM.Service.Models;
using CRM.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly InMemoryRepository<Contact> _repo;

    public ContactController(InMemoryRepository<Contact> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Contact c)
    {
        await _repo.AddAsync(c);
        return Ok(c);
    }
}