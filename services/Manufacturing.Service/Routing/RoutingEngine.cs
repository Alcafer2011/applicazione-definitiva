using AIEnterpriseOS.Manufacturing.Service.Models;

namespace AIEnterpriseOS.Manufacturing.Service.Routing;

public interface IRoutingEngine
{
    AIEnterpriseOS.Manufacturing.Service.Models.Routing Create(AIEnterpriseOS.Manufacturing.Service.Models.Routing routing);
    AIEnterpriseOS.Manufacturing.Service.Models.Routing? Get(string id);
}

public class InMemoryRoutingEngine : IRoutingEngine
{
    private readonly List<AIEnterpriseOS.Manufacturing.Service.Models.Routing> _routes = new();

    public AIEnterpriseOS.Manufacturing.Service.Models.Routing Create(AIEnterpriseOS.Manufacturing.Service.Models.Routing routing)
    {
        _routes.Add(routing);
        return routing;
    }

    public AIEnterpriseOS.Manufacturing.Service.Models.Routing? Get(string id)
        => _routes.FirstOrDefault(x => x.Id == id);
}
