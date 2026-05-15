using Microsoft.AspNetCore.Mvc;

namespace Twin.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            service = "Twin.Service",
            status = "OK",
            timestamp = DateTime.UtcNow
        });
    }
}