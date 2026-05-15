using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.ActionAI.Service.Models;
using AIEnterpriseOS.ActionAI.Service.Services;

namespace AIEnterpriseOS.ActionAI.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionController : ControllerBase
{
    private readonly IActionEngine _engine;

    public ActionController(IActionEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody] ActionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Intent))
            return BadRequest("Intent is required.");

        var exec = await _engine.ExecuteAsync(request);
        return Ok(new { executionId = exec.Id, status = exec.Status.ToString() });
    }

    [HttpGet("status/{id}")]
    public IActionResult Status(string id)
    {
        var exec = _engine.GetStatus(id);
        if (exec is null) return NotFound();

        return Ok(exec);
    }

    [HttpGet("catalog")]
    public IActionResult Catalog()
    {
        var catalog = _engine.GetCatalog();
        return Ok(catalog);
    }
}
