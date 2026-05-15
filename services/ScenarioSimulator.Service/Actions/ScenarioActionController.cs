using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.ScenarioSimulator.Service.Models;
using AIEnterpriseOS.ScenarioSimulator.Service.Combined;

namespace AIEnterpriseOS.ScenarioSimulator.Service.Actions;

[ApiController]
[Route("api/scenario/action")]
public class ScenarioActionController : ControllerBase
{
    private readonly ICombinedScenarioEngine _combined;

    public ScenarioActionController(ICombinedScenarioEngine combined)
    {
        _combined = combined;
    }

    [HttpPost("simulate-combined")]
    public IActionResult SimulateCombined([FromBody] CombinedScenarioRequest request)
    {
        return Ok(_combined.SimulateCombined(request));
    }

    [HttpPost("compare")]
    public IActionResult Compare([FromBody] List<ScenarioResult> results)
    {
        if (results.Count != 2)
            return BadRequest("Provide exactly two scenarios.");

        return Ok(_combined.Compare(results[0], results[1]));
    }
}
