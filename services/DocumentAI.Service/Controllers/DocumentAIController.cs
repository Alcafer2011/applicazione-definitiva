using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.DocumentAI.Service.Services;

namespace AIEnterpriseOS.DocumentAI.Service.Controllers;

[ApiController]
[Route("api/documentai")]
public class DocumentAIController : ControllerBase
{
    private readonly IDocumentEngine _engine;

    public DocumentAIController(IDocumentEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("process")]
    public IActionResult Process([FromBody] byte[] file)
    {
        return Ok(_engine.Process(file));
    }

    [HttpGet("all")]
    public IActionResult All()
    {
        return Ok(_engine.All());
    }
}
