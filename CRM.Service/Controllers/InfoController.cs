using Microsoft.AspNetCore.Mvc;

namespace CRM.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            service = "CRM.Service",
            status = "OK",
            timestamp = DateTime.UtcNow
        });
    }
}