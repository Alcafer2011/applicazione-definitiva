namespace AIEnterpriseOS.IntegrationHub.Service.Models;

public class ExternalEvent
{
    public string Source { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
}

public class IntegrationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
