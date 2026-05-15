namespace AIEnterpriseOS.Tenant.Service.Provisioning;

public interface IProvisioningEngine
{
    string ProvisionTenant(string tenantId);
}

public class BasicProvisioningEngine : IProvisioningEngine
{
    public string ProvisionTenant(string tenantId)
    {
        return $"Tenant {tenantId} provisioned with default modules.";
    }
}
