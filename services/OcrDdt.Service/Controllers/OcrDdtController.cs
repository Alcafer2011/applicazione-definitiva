using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.OcrDdt.Service.Models;

namespace AIEnterpriseOS.OcrDdt.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcrDdtController : ControllerBase
{
    [HttpPost("analyze")]
    public IActionResult Analyze([FromBody] OcrDdtRequest request)
    {
        // TODO: qui colleghi un motore OCR reale (Tesseract, servizio cloud, ecc.)
        var result = new OcrDdtResult
        {
            DocumentNumber = "FAKE-DDT-001",
            DocumentDate = DateTime.UtcNow.Date,
            SupplierName = "Demo Supplier",
            Lines = new List<OcrDdtLine>
            {
                new()
                {
                    SkuOrCode = "ART-001",
                    Description = "Articolo demo",
                    Quantity = 10,
                    UnitOfMeasure = "PZ"
                }
            }
        };

        return Ok(result);
    }
}
