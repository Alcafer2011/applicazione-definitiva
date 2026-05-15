using AIEnterpriseOS.DocumentAI.Service.Models;

namespace AIEnterpriseOS.DocumentAI.Service.Classifiers;

public interface IDocumentClassifier
{
    DocumentType Classify(string text);
}

public class BasicDocumentClassifier : IDocumentClassifier
{
    public DocumentType Classify(string text)
    {
        if (text.Contains("fattura", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("totale", StringComparison.OrdinalIgnoreCase))
            return DocumentType.Invoice;

        if (text.Contains("ordine", StringComparison.OrdinalIgnoreCase))
            return DocumentType.Order;

        if (text.Contains("contratto", StringComparison.OrdinalIgnoreCase))
            return DocumentType.Contract;

        if (text.Contains("ddt", StringComparison.OrdinalIgnoreCase))
            return DocumentType.DeliveryNote;

        return DocumentType.Unknown;
    }
}
