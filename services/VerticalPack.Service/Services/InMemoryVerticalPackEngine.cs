using AIEnterpriseOS.VerticalPack.Service.Models;

namespace AIEnterpriseOS.VerticalPack.Service.Services;

public interface IVerticalPackEngine
{
    IEnumerable<AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack> GetAll();
    AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack? Get(string code);
}

public class InMemoryVerticalPackEngine : IVerticalPackEngine
{
    private readonly List<AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack> _packs = new()
    {
        new AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack
        {
            Code = "ARTIGIANO",
            Name = "Artigiano",
            EnabledModules = new() { "CRM.Service", "Finance.Service", "Warehouse.Service" },
            DefaultWorkflows = new() { "create_quote", "convert_to_invoice" },
            DefaultDashboards = new() { "sales_overview", "cashflow" },
            AiPrompts = new() { "generate_quote", "suggest_price" }
        },
        new AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack
        {
            Code = "EDILIZIA",
            Name = "Edilizia",
            EnabledModules = new() { "CRM.Service", "Finance.Service", "Project.Service", "Warehouse.Service" },
            DefaultWorkflows = new() { "job_order", "material_request" },
            DefaultDashboards = new() { "project_status", "cost_overview" },
            AiPrompts = new() { "estimate_materials", "schedule_team" }
        }
    };

    public IEnumerable<AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack> GetAll() => _packs;

    public AIEnterpriseOS.VerticalPack.Service.Models.VerticalPack? Get(string code)
        => _packs.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
}
