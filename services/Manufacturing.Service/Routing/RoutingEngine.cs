using AIEnterpriseOS.Manufacturing.Service.Models;

namespace AIEnterpriseOS.Manufacturing.Service.Routing;

public interface IRoutingEngine
{
    Routing Create(Routing routing);
    Routing? Get(string id);
}

public class InMemoryRoutingEngine : IRoutingEngine
{
    private readonly List<Routing> _routes = new();

    public Routing Create(Routing routing)
    {
        _routes.Add(routing);
        return routing;
    }

    public Routing? Get(string id)
        => _routes.FirstOrDefault(x => x.Id == id);
}
