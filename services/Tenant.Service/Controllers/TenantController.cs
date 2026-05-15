using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Tenant.Service.Models;
using AIEnterpriseOS.Tenant.Service.Services;

namespace AIEnterpriseOS.Tenant.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private readonly ITenantEngine _engine;

    public TenantController(ITenantEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("provision")]
    public IActionResult Provision([FromBody] TenantProvisionRequest request)
    {
        return Ok(_engine.Provision(request));
    }

    [HttpGet("{id}")]
    public IActionResult GetTenant(string id)
    {
        var t = _engine.GetTenant(id);
        if (t is null) return NotFound();
        return Ok(t);
    }

    [HttpGet]
    public IActionResult GetAllTenants()
    {
        return Ok(_engine.GetAllTenants());
    }
}
