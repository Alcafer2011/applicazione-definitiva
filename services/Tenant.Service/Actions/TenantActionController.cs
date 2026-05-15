using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Tenant.Service.Services;
using AIEnterpriseOS.Tenant.Service.Provisioning;

namespace AIEnterpriseOS.Tenant.Service.Actions;

[ApiController]
[Route("api/tenant/action")]
public class TenantActionController : ControllerBase
{
    private readonly IUsageEngine _usage;
    private readonly IProvisioningEngine _provisioning;

    public TenantActionController(IUsageEngine usage, IProvisioningEngine provisioning)
    {
        _usage = usage;
        _provisioning = provisioning;
    }

    [HttpPost("track")]
    public IActionResult TrackUsage(string tenantId, string module, decimal tokens = 0)
    {
        _usage.Track(tenantId, module, tokens);
        return Ok("Usage tracked.");
    }

    [HttpPost("provision/{tenantId}")]
    public IActionResult Provision(string tenantId)
    {
        return Ok(_provisioning.ProvisionTenant(tenantId));
    }
}
