using AIEnterpriseOS.Finance.Service.Models;

namespace AIEnterpriseOS.Finance.Service.Services;

public interface IFinanceEngine
{
    Invoice CreateInvoice(Invoice invoice);
    Invoice? GetInvoice(string id);
    IEnumerable<Invoice> GetAllInvoices();
    Payment RegisterPayment(Payment payment);
    IEnumerable<BankStatementRecord> ImportBankStatement(IEnumerable<BankStatementRecord> records);
}

public class InMemoryFinanceEngine : IFinanceEngine
{
    private readonly List<Invoice> _invoices = new();
    private readonly List<Payment> _payments = new();
    private readonly List<BankStatementRecord> _bank = new();

    public Invoice CreateInvoice(Invoice invoice)
    {
        _invoices.Add(invoice);
        return invoice;
    }

    public Invoice? GetInvoice(string id)
        => _invoices.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Invoice> GetAllInvoices()
        => _invoices;

    public Payment RegisterPayment(Payment payment)
    {
        _payments.Add(payment);

        var invoice = _invoices.FirstOrDefault(x => x.Id == payment.InvoiceId);
        if (invoice != null)
            invoice.Status = InvoiceStatus.Paid;

        return payment;
    }

    public IEnumerable<BankStatementRecord> ImportBankStatement(IEnumerable<BankStatementRecord> records)
    {
        _bank.AddRange(records);
        return records;
    }
}
