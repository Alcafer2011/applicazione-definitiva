using Microsoft.AspNetCore.Mvc;
using MemoryGraph.Service.Models;
using MemoryGraph.Service.Core;

namespace MemoryGraph.Service.Controllers;

[ApiController]
[Route("memorygraph")]
public class MemoryGraphController : ControllerBase
{
    private readonly MemoryGraphCore _core = new();

    [HttpPost("node")]
    public async Task<IActionResult> AddNode(GraphNode node)
    {
        var result = await _core.AddNode(node);
        return Ok(result);
    }

    [HttpPost("relation")]
    public async Task<IActionResult> AddRelation(GraphRelation rel)
    {
        var result = await _core.AddRelation(rel);
        return Ok(result);
    }

    [HttpGet("related/{id}")]
    public async Task<IActionResult> Related(string id)
    {
        var result = await _core.GetRelated(id);
        return Ok(result);
    }
}