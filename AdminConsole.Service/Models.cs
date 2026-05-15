namespace AdminConsole.Service.Models;

public class AdminUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = "";
    public List<string> Roles { get; set; } = new();
    public bool Active { get; set; } = true;
}

public class AdminRole
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public List<string> Permissions { get; set; } = new();
}

public class LicenseInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Key { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public bool Active { get; set; }
}

public class AiRule
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Agent { get; set; } = "";
    public bool Enabled { get; set; }
    public int Priority { get; set; }
}