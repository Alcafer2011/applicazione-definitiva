using System.Net.Http.Json;
using Dashboard.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Service.Controllers;

[ApiController]
[Route("dashboard")]
public class DashboardController : ControllerBase
{
    private readonly HttpClient _http;

    public DashboardController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient();
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        var summary = new DashboardSummary();

        try
        {
            // BI: KPI + Forecast
            summary.Kpi      = await SafeGet("http://bi:8080/bi/kpi");
            summary.Forecast = await SafeGet("http://bi:8080/bi/forecast");

            // Auto-Healing: stato servizi
            summary.AutoHealingStatus = await SafeGet("http://autohealing:8080/autohealing/status");

            // Multi-AI: insight generico
            var aiReq = new
            {
                Goal = "insight",
                Context = new Dictionary<string, object>()
            };
            summary.MultiAIInsight = await SafePost("http://multiai:8080/multiai/process", aiReq);

            // Market Intelligence: strategia sul primo competitor (se esiste)
            var competitors = await SafeGet("http://marketintelligence:8080/market/competitors");
            summary.MarketStrategy = competitors;
        }
        catch
        {
            // non blocchiamo la dashboard se qualcosa fallisce
        }

        return Ok(summary);
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
        catch
        {
            return null;
        }
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
        catch
        {
            return null;
        }
    }
}