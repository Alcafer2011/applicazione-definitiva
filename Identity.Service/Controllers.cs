using Microsoft.AspNetCore.Mvc;
using Identity.Service.Models;
using Identity.Service.Core;

namespace Identity.Service.Controllers;

[ApiController]
[Route("identity")]
public class IdentityController : ControllerBase
{
    private readonly IdentityCore _core = new();

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var token = await _core.Login(req);
        if (token == null) return Unauthorized("Credenziali non valide");
        return Ok(token);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(string username, string password, string roles)
    {
        var user = await _core.CreateUser(username, password, roles.Split(',').ToList());
        return Ok(user);
    }
}