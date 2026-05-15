using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Onboarding.Service.Models;
using AIEnterpriseOS.Onboarding.Service.Services;

namespace AIEnterpriseOS.Onboarding.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OnboardingController : ControllerBase
{
    private readonly IOnboardingEngine _engine;

    public OnboardingController(IOnboardingEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("profile")]
    public IActionResult CreateProfile([FromBody] CompanyProfile profile)
    {
        var result = _engine.CreateProfile(profile);
        return Ok(result);
    }

    [HttpPost("import")]
    public IActionResult Import([FromBody] ImportRequest request)
    {
        var result = _engine.ImportData(request);
        return Ok(result);
    }
}
