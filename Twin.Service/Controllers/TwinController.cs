using Microsoft.AspNetCore.Mvc;
using Twin.Service.Models;
using Twin.Service.Repositories;

namespace Twin.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TwinController : ControllerBase
{
    private readonly InMemoryRepository<SystemNode> _nodes;

    public TwinController(InMemoryRepository<SystemNode> nodes)
    {
        _nodes = nodes;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _nodes.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SystemNode node)
    {
        await _nodes.AddAsync(node);
        return Ok(node);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SystemNode node)
    {
        await _nodes.UpdateAsync(node);
        return Ok(node);
    }
}