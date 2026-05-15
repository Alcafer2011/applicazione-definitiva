using AIEnterpriseOS.Tenant.Service.Usage;

namespace AIEnterpriseOS.Tenant.Service.Services;

public interface IUsageEngine
{
    void Track(string tenantId, string module, decimal tokens = 0);
    TenantUsage GetUsage(string tenantId);
}

public class InMemoryUsageEngine : IUsageEngine
{
    private readonly List<TenantUsage> _usages = new();

    public void Track(string tenantId, string module, decimal tokens = 0)
    {
        var usage = _usages.FirstOrDefault(x => x.TenantId == tenantId);
        if (usage == null)
        {
            usage = new TenantUsage { TenantId = tenantId };
            _usages.Add(usage);
        }

        var mod = usage.Modules.FirstOrDefault(x => x.Module == module);
        if (mod == null)
        {
            mod = new ModuleUsage { Module = module };
            usage.Modules.Add(mod);
        }

        mod.Calls++;
        mod.AiTokensUsed += tokens;
        mod.LastUsed = DateTime.UtcNow;
    }

    public TenantUsage GetUsage(string tenantId)
        => _usages.FirstOrDefault(x => x.TenantId == tenantId) ?? new TenantUsage { TenantId = tenantId };
}
