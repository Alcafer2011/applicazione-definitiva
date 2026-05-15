using CRM.Service.Models;
using CRM.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly InMemoryRepository<Company> _repo;

    public CompanyController(InMemoryRepository<Company> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _repo.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Company c)
    {
        await _repo.AddAsync(c);
        return Ok(c);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Company c)
    {
        await _repo.UpdateAsync(c);
        return Ok(c);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _repo.DeleteAsync(id);
        return Ok();
    }
}