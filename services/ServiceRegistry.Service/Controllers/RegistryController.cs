using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.ServiceRegistry.Service.Models;

namespace AIEnterpriseOS.ServiceRegistry.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistryController : ControllerBase
{
    [HttpGet("scan")]
    public IActionResult Scan()
    {
        var root = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "services");
        if (!Directory.Exists(root))
            return Ok(new List<RegisteredService>());

        var dirs = Directory.GetDirectories(root)
            .Where(d => d.EndsWith(".Service"))
            .Select(d => new RegisteredService
            {
                Name = Path.GetFileName(d),
                Path = d,
                Url = $"http://{Path.GetFileName(d).ToLower().Replace(".service","")}:8080"
            })
            .ToList();

        return Ok(dirs);
    }
}
