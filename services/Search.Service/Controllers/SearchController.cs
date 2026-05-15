using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Search.Service.Services;

namespace AIEnterpriseOS.Search.Service.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchEngine _engine;

    public SearchController(ISearchEngine engine)
    {
        _engine = engine;
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string q)
    {
        return Ok(_engine.Search(q));
    }
}
