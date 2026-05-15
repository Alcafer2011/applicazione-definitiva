using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.VerticalPack.Service.Services;

namespace AIEnterpriseOS.VerticalPack.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerticalController : ControllerBase
{
    private readonly IVerticalPackEngine _engine;

    public VerticalController(IVerticalPackEngine engine)
    {
        _engine = engine;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_engine.GetAll());

    [HttpGet("{code}")]
    public IActionResult Get(string code)
    {
        var pack = _engine.Get(code);
        if (pack is null) return NotFound();
        return Ok(pack);
    }
}
