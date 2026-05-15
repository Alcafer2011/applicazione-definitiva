namespace Orchestrator.Service.Models;

public record RouteRequest(string Module, string Action, object? Payload);
public record RouteResult(bool Success, object? Data, string? Error);