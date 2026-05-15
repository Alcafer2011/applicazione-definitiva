namespace ApiGateway.Service.Models;

public class RouteRule
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PathPrefix { get; set; } = "";
    public string Target { get; set; } = "";
    public List<string> AllowedRoles { get; set; } = new();
}