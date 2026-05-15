using Microsoft.AspNetCore.Mvc;

namespace Governance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            service = "Governance.Service",
            status = "OK",
            timestamp = DateTime.UtcNow
        });
    }
}