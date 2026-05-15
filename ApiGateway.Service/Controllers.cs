using Microsoft.AspNetCore.Mvc;
using ApiGateway.Service.Models;
using ApiGateway.Service.Core;

namespace ApiGateway.Service.Controllers;

[ApiController]
[Route("gateway")]
public class GatewayController : ControllerBase
{
    private readonly GatewayCore _core = new();

    [HttpGet("rules")]
    public async Task<IActionResult> Rules()
    {
        var rules = await _core.GetRules();
        return Ok(rules);
    }
}