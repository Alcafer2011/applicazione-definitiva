using Microsoft.AspNetCore.Mvc;
using SearchEngine.Service.Core;

namespace SearchEngine.Service.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly SearchCore _core = new();

    [HttpGet("query")]
    public async Task<IActionResult> Query([FromQuery] string q)
    {
        var result = await _core.Query(q);
        return Ok(result);
    }

    [HttpPost("index/rebuild")]
    public async Task<IActionResult> Rebuild()
    {
        await _core.RebuildIndex();
        return Ok(new { status = "index rebuilt" });
    }
}