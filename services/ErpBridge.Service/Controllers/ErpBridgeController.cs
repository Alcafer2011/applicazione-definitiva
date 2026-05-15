using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.ErpBridge.Service.Models;

namespace AIEnterpriseOS.ErpBridge.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErpBridgeController : ControllerBase
{
    // TODO: in futuro: persistenza su DB
    private static readonly List<ErpSystemConfig> _configs = new();

    [HttpPost("config")]
    public IActionResult AddOrUpdateConfig([FromBody] ErpSystemConfig config)
    {
        var existing = _configs.FirstOrDefault(c => c.Id == config.Id);
        if (existing is null)
        {
            config.Id = string.IsNullOrWhiteSpace(config.Id) ? Guid.NewGuid().ToString() : config.Id;
            _configs.Add(config);
        }
        else
        {
            existing.Name = config.Name;
            existing.Type = config.Type;
            existing.ApiBaseUrl = config.ApiBaseUrl;
            existing.ApiKey = config.ApiKey;
        }

        return Ok(config);
    }

    [HttpGet("config")]
    public IActionResult GetConfigs() => Ok(_configs);

    [HttpPost("import-invoices")]
    public IActionResult ImportInvoices([FromBody] ErpInvoiceImportRequest request)
    {
        // TODO: qui colleghi davvero il gestionale esterno
        var fake = new List<ErpInvoiceSummary>
        {
            new()
            {
                InvoiceNumber = "FAKE-001",
                InvoiceDate = DateTime.UtcNow.Date,
                CustomerName = "Demo Customer",
                TotalAmount = 1234.56m,
                Currency = "EUR"
            }
        };

        return Ok(fake);
    }
}
