using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.IntegrationHub.Service.Models;

namespace AIEnterpriseOS.IntegrationHub.Service.Webhooks;

[ApiController]
[Route("api/integration/webhook")]
public class WebhookController : ControllerBase
{
    [HttpPost("{source}")]
    public IActionResult Receive(string source, [FromBody] object payload)
    {
        var evt = new ExternalEvent
        {
            Source = source,
            EventType = "webhook",
            Payload = payload.ToString() ?? ""
        };

        return Ok(new { message = "Webhook received", evt });
    }
}
