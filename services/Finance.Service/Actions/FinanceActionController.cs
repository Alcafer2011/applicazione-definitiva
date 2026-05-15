using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Finance.Service.Models;
using AIEnterpriseOS.Finance.Service.Services;

namespace AIEnterpriseOS.Finance.Service.Actions;

[ApiController]
[Route("api/finance/action")]
public class FinanceActionController : ControllerBase
{
    private readonly IFinanceEngine _engine;

    public FinanceActionController(IFinanceEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("create-invoice")]
    public IActionResult CreateInvoiceAction([FromBody] Invoice invoice)
    {
        var result = _engine.CreateInvoice(invoice);
        return Ok(new { message = "Invoice created", invoiceId = result.Id });
    }

    [HttpPost("register-payment")]
    public IActionResult RegisterPaymentAction([FromBody] Payment payment)
    {
        var result = _engine.RegisterPayment(payment);
        return Ok(new { message = "Payment registered", paymentId = result.Id });
    }

    [HttpPost("generate-schedule/{invoiceId}")]
    public IActionResult GenerateSchedule(string invoiceId)
    {
        var invoice = _engine.GetInvoice(invoiceId);
        if (invoice is null) return NotFound();

        var due = new DueDate
        {
            Date = invoice.IssueDate.AddDays(30),
            Amount = invoice.Total
        };

        invoice.DueDates.Add(due);

        return Ok(new { message = "Schedule generated", dueDate = due });
    }
}
