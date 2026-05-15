using Microsoft.AspNetCore.Mvc;

namespace Orchestrator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            service = "Orchestrator.Service",
            status = "OK",
            timestamp = DateTime.UtcNow
        });
    }
}