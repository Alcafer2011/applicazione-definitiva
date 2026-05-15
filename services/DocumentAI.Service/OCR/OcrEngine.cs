namespace AIEnterpriseOS.DocumentAI.Service.OCR;

public interface IOcrEngine
{
    string ExtractText(byte[] file);
}

public class FakeOcrEngine : IOcrEngine
{
    public string ExtractText(byte[] file)
    {
        return "FAKE OCR TEXT: Totale 1200€, Cliente Rossi SRL, Data 12/05/2026";
    }
}
