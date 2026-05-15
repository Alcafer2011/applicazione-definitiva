namespace Governance.Service.Models;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string UserId, string RoleId);