namespace Governance.Service.Models;

public class Permission
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
}