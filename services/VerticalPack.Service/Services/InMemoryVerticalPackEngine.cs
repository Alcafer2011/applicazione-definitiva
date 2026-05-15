using AIEnterpriseOS.VerticalPack.Service.Models;

namespace AIEnterpriseOS.VerticalPack.Service.Services;

public interface IVerticalPackEngine
{
    IEnumerable<VerticalPack> GetAll();
    VerticalPack? Get(string code);
}

public class InMemoryVerticalPackEngine : IVerticalPackEngine
{
    private readonly List<VerticalPack> _packs = new()
    {
        new VerticalPack
        {
            Code = "ARTIGIANO",
            Name = "Artigiano",
            EnabledModules = new() { "CRM.Service", "Finance.Service", "Warehouse.Service" },
            DefaultWorkflows = new() { "create_quote", "convert_to_invoice" },
            DefaultDashboards = new() { "sales_overview", "cashflow" },
            AiPrompts = new() { "generate_quote", "suggest_price" }
        },
        new VerticalPack
        {
            Code = "EDILIZIA",
            Name = "Edilizia",
            EnabledModules = new() { "CRM.Service", "Finance.Service", "Project.Service", "Warehouse.Service" },
            DefaultWorkflows = new() { "job_order", "material_request" },
            DefaultDashboards = new() { "project_status", "cost_overview" },
            AiPrompts = new() { "estimate_materials", "schedule_team" }
        }
    };

    public IEnumerable<VerticalPack> GetAll() => _packs;

    public VerticalPack? Get(string code)
        => _packs.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
}
