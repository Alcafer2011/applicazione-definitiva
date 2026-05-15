using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.ScenarioSimulator.Service.Models;
using AIEnterpriseOS.ScenarioSimulator.Service.Services;

namespace AIEnterpriseOS.ScenarioSimulator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScenarioController : ControllerBase
{
    private readonly IScenarioEngine _engine;

    public ScenarioController(IScenarioEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("simulate")]
    public IActionResult Simulate([FromBody] ScenarioRequest request)
    {
        var result = _engine.Simulate(request);
        return Ok(result);
    }

    [HttpGet("result/{id}")]
    public IActionResult GetResult(string id)
    {
        var r = _engine.GetResult(id);
        if (r is null) return NotFound();
        return Ok(r);
    }
}
