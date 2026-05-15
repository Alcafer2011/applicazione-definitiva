using Licensing.Service.Models;
using Licensing.Service.Security;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.Service.Controllers;

[ApiController]
[Route("licensing")]
public class LicensingController : ControllerBase
{
    private static readonly LicenseSigner Signer = new();

    [HttpPost("issue")]
    public IActionResult Issue([FromBody] LicenseRequest request)
    {
        var payload = new LicensePayload
        {
            CustomerName = request.CustomerName,
            MachineId = request.MachineId,
            ExpiresAt = request.ExpiresAt.ToUniversalTime(),
            Modules = request.Modules
        };

        var lic = Signer.Issue(payload);
        return Ok(lic);
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] LicenseEnvelope envelope, [FromQuery] string machineId)
    {
        if (!Signer.Validate(envelope, out var payload) || payload == null)
            return Unauthorized("Licenza non valida");

        if (!string.Equals(payload.MachineId, machineId, StringComparison.OrdinalIgnoreCase))
            return Unauthorized("MachineId non corrisponde");

        return Ok(new
        {
            Valid = true,
            payload.CustomerName,
            payload.MachineId,
            payload.ExpiresAt,
            payload.Modules
        });
    }
}