namespace Governance.Service.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}