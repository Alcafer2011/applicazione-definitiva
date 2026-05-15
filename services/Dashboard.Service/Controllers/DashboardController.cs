using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Dashboard.Service.Models;
using AIEnterpriseOS.Dashboard.Service.Services;
using AIEnterpriseOS.Dashboard.Service.Widgets;

namespace AIEnterpriseOS.Dashboard.Service.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly ILayoutEngine _layouts;
    private readonly IWidgetEngine _widgets;

    public DashboardController(ILayoutEngine layouts, IWidgetEngine widgets)
    {
        _layouts = layouts;
        _widgets = widgets;
    }

    [HttpGet("{userId}")]
    public IActionResult Get(string userId)
    {
        var layout = _layouts.Get(userId);
        if (layout is null) return NotFound();
        return Ok(layout);
    }

    [HttpPost("save")]
    public IActionResult Save([FromBody] DashboardLayout layout)
    {
        return Ok(_layouts.Save(layout));
    }

    [HttpPost("render")]
    public IActionResult Render([FromBody] Widget widget)
    {
        return Ok(_widgets.Render(widget));
    }
}
