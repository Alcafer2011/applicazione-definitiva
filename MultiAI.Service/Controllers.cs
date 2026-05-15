using Microsoft.AspNetCore.Mvc;
using MultiAI.Service.Models;
using MultiAI.Service.Core;

namespace MultiAI.Service.Controllers;

[ApiController]
[Route("multiai")]
public class MultiAIController : ControllerBase
{
    private readonly BInchenninnso _core;

    public MultiAIController()
    {
        var agents = new List<IAgent>
        {
            new InsightAgent(),
            new ForecastAgent(),
            new GovernanceAgent()
        };

        _core = new BInchenninnso(agents);
    }

    [HttpPost("process")]
    public IActionResult Process(AIRequest request)
    {
        var result = _core.Process(request);
        return Ok(result);
    }
}