using Microsoft.AspNetCore.Mvc;
using BI.Service.Models;
using MongoDB.Driver;

namespace BI.Service.Controllers;

[ApiController]
[Route("bi")]
public class BIController : ControllerBase
{
    private readonly IMongoDatabase _db;

    public BIController()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        _db = client.GetDatabase("bos-bi");
    }

    [HttpGet("kpi")]
    public IActionResult GetKpi()
    {
        var results = new List<KpiResult>
        {
            new() { Name = "OrdiniUltimi30Giorni", Value = 128 },
            new() { Name = "ClientiAttivi", Value = 42 },
            new() { Name = "TicketAperti", Value = 7 }
        };

        return Ok(results);
    }

    [HttpGet("forecast")]
    public IActionResult Forecast()
    {
        var result = new ForecastResult
        {
            Metric = "Vendite",
            Predictions = new List<double> { 120, 135, 150, 162 }
        };

        return Ok(result);
    }
}