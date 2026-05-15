using Microsoft.AspNetCore.Mvc;
using MarketIntelligence.Service.Models;
using MarketIntelligence.Service.Core;

namespace MarketIntelligence.Service.Controllers;

[ApiController]
[Route("market")]
public class MarketController : ControllerBase
{
    private readonly MarketCore _core = new();

    [HttpGet("competitors")]
    public async Task<IActionResult> GetCompetitors()
    {
        var result = await _core.GetCompetitors();
        return Ok(result);
    }

    [HttpPost("competitors")]
    public async Task<IActionResult> AddCompetitor(Competitor c)
    {
        var result = await _core.AddCompetitor(c);
        return Ok(result);
    }

    [HttpGet("strategy/{name}")]
    public async Task<IActionResult> Strategy(string name)
    {
        var list = await _core.GetCompetitors();
        var competitor = list.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (competitor == null)
            return NotFound("Competitor non trovato");

        var strategy = _core.GenerateStrategy(competitor);
        return Ok(strategy);
    }
}