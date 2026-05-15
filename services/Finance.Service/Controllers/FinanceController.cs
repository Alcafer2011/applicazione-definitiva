using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Finance.Service.Models;
using AIEnterpriseOS.Finance.Service.Services;

namespace AIEnterpriseOS.Finance.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceEngine _engine;

    public FinanceController(IFinanceEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("invoice")]
    public IActionResult CreateInvoice([FromBody] Invoice invoice)
    {
        var result = _engine.CreateInvoice(invoice);
        return Ok(result);
    }

    [HttpGet("invoice/{id}")]
    public IActionResult GetInvoice(string id)
    {
        var invoice = _engine.GetInvoice(id);
        if (invoice is null) return NotFound();
        return Ok(invoice);
    }

    [HttpGet("invoices")]
    public IActionResult GetAllInvoices()
    {
        return Ok(_engine.GetAllInvoices());
    }

    [HttpPost("payment")]
    public IActionResult RegisterPayment([FromBody] Payment payment)
    {
        var result = _engine.RegisterPayment(payment);
        return Ok(result);
    }

    [HttpPost("bank/import")]
    public IActionResult ImportBank([FromBody] IEnumerable<BankStatementRecord> records)
    {
        var result = _engine.ImportBankStatement(records);
        return Ok(result);
    }
}
