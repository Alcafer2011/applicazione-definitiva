using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.IntegrationHub.Service.Connectors;

namespace AIEnterpriseOS.IntegrationHub.Service.Controllers;

[ApiController]
[Route("api/integration/action")]
public class IntegrationActionController : ControllerBase
{
    private readonly ConnectorRegistry _registry;

    public IntegrationActionController(ConnectorRegistry registry)
    {
        _registry = registry;
    }

    [HttpPost("send/{connector}")]
    public IActionResult Send(string connector, [FromBody] string payload)
    {
        var c = _registry.Get(connector);
        if (c is null) return NotFound("Connector not found");

        return Ok(c.Send(payload));
    }
}
