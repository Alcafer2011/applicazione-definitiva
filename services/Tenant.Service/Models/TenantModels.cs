namespace AIEnterpriseOS.Tenant.Service.Models;

public enum TenantPlan
{
    Free,
    Basic,
    Pro,
    Enterprise
}

public class TenantModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CompanyName { get; set; } = string.Empty;
    public TenantPlan Plan { get; set; } = TenantPlan.Free;
    public List<string> EnabledModules { get; set; } = new();
    public int MaxUsers { get; set; }
    public int MaxStorageMb { get; set; }
    public int MaxAiTokens { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class TenantProvisionRequest
{
    public string CompanyName { get; set; } = string.Empty;
    public TenantPlan Plan { get; set; }
    public string VerticalCode { get; set; } = string.Empty;
}
