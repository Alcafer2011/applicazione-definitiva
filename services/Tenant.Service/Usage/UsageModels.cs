namespace AIEnterpriseOS.Tenant.Service.Usage;

public class ModuleUsage
{
    public string Module { get; set; } = string.Empty;
    public int Calls { get; set; }
    public decimal AiTokensUsed { get; set; }
    public DateTime LastUsed { get; set; } = DateTime.UtcNow;
}

public class TenantUsage
{
    public string TenantId { get; set; } = string.Empty;
    public List<ModuleUsage> Modules { get; set; } = new();
    public decimal StorageUsedMb { get; set; }
    public int ActiveUsers { get; set; }
}
