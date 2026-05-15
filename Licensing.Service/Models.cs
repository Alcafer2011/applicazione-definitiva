namespace Licensing.Service.Models;

public class LicenseRequest
{
    public string CustomerName { get; set; } = "";
    public string MachineId { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public List<string> Modules { get; set; } = new();
}

public class LicensePayload
{
    public string CustomerName { get; set; } = "";
    public string MachineId { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public List<string> Modules { get; set; } = new();
}

public class LicenseEnvelope
{
    public string Payload { get; set; } = "";
    public string Signature { get; set; } = "";
}