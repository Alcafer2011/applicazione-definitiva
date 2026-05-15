using Microsoft.AspNetCore.Mvc;
using StatusMonitor.Service.Models;

namespace StatusMonitor.Service.Controllers;

[ApiController]
[Route("status")]
public class StatusController : ControllerBase
{
    private readonly HttpClient _http;

    public StatusController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient();
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        var status = new SystemStatus();

        status.Services     = await SafeGet("http://autohealing:8080/autohealing/status");
        status.AutoHealing  = status.Services;
        status.MultiAI      = await SafePost("http://multiai:8080/multiai/process", new { Goal = "insight", Context = new Dictionary<string, object>() });
        status.MemoryGraph  = await SafeGet("http://memorygraph:8080/memorygraph/related/root");
        status.Market       = await SafeGet("http://marketintelligence:8080/market/competitors");
        status.Workflow     = await SafeGet("http://workflow:8080/workflow/instances");
        status.Licensing    = await SafeGet("http://licensing:8080/licensing/validate?machineId=test");
        status.BI           = await SafeGet("http://bi:8080/bi/kpi");

        return Ok(status);
    }

    private async Task<object?> SafeGet(string url)
    {
        try
        {
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            var json = await resp.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<object>(json);
        }
        catch { return null; }
    }

    private async Task<object?> SafePost(string url, object body)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync(url, body);
            if (!resp.IsSuccessStatusCode) return null;
            var json = await resp.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<object>(json);
        }
        catch { return null; }
    }
}