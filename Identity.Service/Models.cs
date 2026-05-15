namespace Identity.Service.Models;

public class UserAccount
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public List<string> Roles { get; set; } = new();
    public bool Active { get; set; } = true;
}

public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class TokenResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}