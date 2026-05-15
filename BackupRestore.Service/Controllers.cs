using Microsoft.AspNetCore.Mvc;
using BackupRestore.Service.Core;

namespace BackupRestore.Service.Controllers;

[ApiController]
[Route("backup")]
public class BackupController : ControllerBase
{
    private readonly BackupCore _core = new();

    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        var result = await _core.CreateBackup();
        return Ok(result);
    }

    [HttpPost("restore")]
    public async Task<IActionResult> Restore([FromQuery] string file)
    {
        var ok = await _core.Restore(file);
        return Ok(new { restored = ok });
    }
}