using AIEnterpriseOS.Tenant.Service.Marketplace;

namespace AIEnterpriseOS.Tenant.Service.Services;

public interface IMarketplaceEngine
{
    IEnumerable<MarketplaceModule> ListModules();
}

public class InMemoryMarketplaceEngine : IMarketplaceEngine
{
    private readonly List<MarketplaceModule> _modules = new()
    {
        new MarketplaceModule { Code = "AI.Premium", Name = "AI Premium Pack", PricePerMonth = 49 },
        new MarketplaceModule { Code = "CRM.Advanced", Name = "CRM Advanced", PricePerMonth = 29 },
        new MarketplaceModule { Code = "Finance.Pro", Name = "Finance Pro", PricePerMonth = 39 }
    };

    public IEnumerable<MarketplaceModule> ListModules() => _modules;
}
