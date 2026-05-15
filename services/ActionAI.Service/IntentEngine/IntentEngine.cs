namespace AIEnterpriseOS.ActionAI.Service.IntentEngine;

public class IntentResult
{
    public string Intent { get; set; } = string.Empty;
    public Dictionary<string, string> Entities { get; set; } = new();
    public string Module { get; set; } = string.Empty;
}

public interface IIntentEngine
{
    IntentResult Analyze(string input);
}

public class BasicIntentEngine : IIntentEngine
{
    public IntentResult Analyze(string input)
    {
        var result = new IntentResult();

        if (input.Contains("fattura") || input.Contains("invoice"))
        {
            result.Intent = "create_invoice";
            result.Module = "Finance.Service";
        }
        else if (input.Contains("messaggio") || input.Contains("message"))
        {
            result.Intent = "send_message";
            result.Module = "CustomerHub.Service";
        }
        else
        {
            result.Intent = "unknown";
            result.Module = "none";
        }

        return result;
    }
}
