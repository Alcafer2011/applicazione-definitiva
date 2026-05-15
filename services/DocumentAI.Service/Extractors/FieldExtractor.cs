using AIEnterpriseOS.DocumentAI.Service.Models;

namespace AIEnterpriseOS.DocumentAI.Service.Extractors;

public interface IFieldExtractor
{
    Dictionary<string, string> Extract(DocumentType type, string text);
}

public class BasicFieldExtractor : IFieldExtractor
{
    public Dictionary<string, string> Extract(DocumentType type, string text)
    {
        var fields = new Dictionary<string, string>();

        if (type == DocumentType.Invoice)
        {
            fields["Total"] = "1200";
            fields["Customer"] = "Rossi SRL";
            fields["Date"] = "2026-05-12";
        }

        if (type == DocumentType.Order)
        {
            fields["OrderNumber"] = "ORD-001";
            fields["Customer"] = "Rossi SRL";
        }

        return fields;
    }
}
