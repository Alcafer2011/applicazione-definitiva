using Orchestrator.Service.Models;

namespace Orchestrator.Service.Services;

public interface IOrchestratorRouter
{
    Task<RouteResult> RouteAsync(RouteRequest request, CancellationToken ct = default);
}