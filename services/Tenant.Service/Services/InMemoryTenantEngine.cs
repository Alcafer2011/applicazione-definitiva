using AIEnterpriseOS.Tenant.Service.Models;

namespace AIEnterpriseOS.Tenant.Service.Services;

public interface ITenantEngine
{
    TenantModel Provision(TenantProvisionRequest request);
    TenantModel? GetTenant(string id);
    IEnumerable<TenantModel> GetAllTenants();
}

public class InMemoryTenantEngine : ITenantEngine
{
    private readonly List<TenantModel> _tenants = new();

    public TenantModel Provision(TenantProvisionRequest request)
    {
        var tenant = new TenantModel
        {
            CompanyName = request.CompanyName,
            Plan = request.Plan,
            EnabledModules = GetModulesForPlan(request.Plan),
            MaxUsers = GetMaxUsers(request.Plan),
            MaxStorageMb = GetMaxStorage(request.Plan),
            MaxAiTokens = GetMaxAiTokens(request.Plan)
        };

        _tenants.Add(tenant);
        return tenant;
    }

    public TenantModel? GetTenant(string id)
        => _tenants.FirstOrDefault(x => x.Id == id);

    public IEnumerable<TenantModel> GetAllTenants()
        => _tenants;

    private List<string> GetModulesForPlan(TenantPlan plan)
    {
        return plan switch
        {
            TenantPlan.Free => new() { "CRM.Service" },
            TenantPlan.Basic => new() { "CRM.Service", "Finance.Service" },
            TenantPlan.Pro => new() { "CRM.Service", "Finance.Service", "Warehouse.Service", "Project.Service" },
            TenantPlan.Enterprise => new() { "CRM.Service", "Finance.Service", "Warehouse.Service", "Project.Service", "ScenarioSimulator.Service", "ActionAI.Service" },
            _ => new()
        };
    }

    private int GetMaxUsers(TenantPlan plan)
    {
        return plan switch
        {
            TenantPlan.Free => 1,
            TenantPlan.Basic => 5,
            TenantPlan.Pro => 20,
            TenantPlan.Enterprise => 9999,
            _ => 1
        };
    }

    private int GetMaxStorage(TenantPlan plan)
    {
        return plan switch
        {
            TenantPlan.Free => 100,
            TenantPlan.Basic => 1000,
            TenantPlan.Pro => 5000,
            TenantPlan.Enterprise => 100000,
            _ => 100
        };
    }

    private int GetMaxAiTokens(TenantPlan plan)
    {
        return plan switch
        {
            TenantPlan.Free => 1000,
            TenantPlan.Basic => 10000,
            TenantPlan.Pro => 50000,
            TenantPlan.Enterprise => 999999,
            _ => 1000
        };
    }
}
