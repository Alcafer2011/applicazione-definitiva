namespace Governance.Service.Models;

public class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public List<string> Permissions { get; set; } = new();
}