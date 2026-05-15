using AIEnterpriseOS.IntegrationHub.Service.Models;

namespace AIEnterpriseOS.IntegrationHub.Service.Connectors;

public interface IConnector
{
    string Name { get; }
    IntegrationResult Send(string payload);
}

public class ShopifyConnector : IConnector
{
    public string Name => "Shopify";

    public IntegrationResult Send(string payload)
    {
        return new IntegrationResult
        {
            Success = true,
            Message = "Payload sent to Shopify"
        };
    }
}

public class StripeConnector : IConnector
{
    public string Name => "Stripe";

    public IntegrationResult Send(string payload)
    {
        return new IntegrationResult
        {
            Success = true,
            Message = "Payment event sent to Stripe"
        };
    }
}

public class ConnectorRegistry
{
    public List<IConnector> Connectors { get; } = new()
    {
        new ShopifyConnector(),
        new StripeConnector()
    };

    public IConnector? Get(string name)
        => Connectors.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}
