using Microsoft.AspNetCore.Mvc;

namespace DigitalBrain.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            service = "DigitalBrain.Service",
            status = "OK",
            timestamp = DateTime.UtcNow
        });
    }
}