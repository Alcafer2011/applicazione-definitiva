using System.Net.Http.Json;
using Orchestrator.Service.Models;

namespace Orchestrator.Service.Services;

public class OrchestratorRouter : IOrchestratorRouter
{
    private readonly IHttpClientFactory _http;

    public OrchestratorRouter(IHttpClientFactory http)
    {
        _http = http;
    }

    public async Task<RouteResult> RouteAsync(RouteRequest request, CancellationToken ct = default)
    {
        try
        {
            if (request.Module.ToLower() == "brain")
            {
                var client = _http.CreateClient("digitalbrain");
                var response = await client.PostAsJsonAsync($"api/reasoning", request.Payload, ct);

                if (!response.IsSuccessStatusCode)
                    return new RouteResult(false, null, "Errore DigitalBrain");

                var data = await response.Content.ReadFromJsonAsync<object>(cancellationToken: ct);
                return new RouteResult(true, data, null);
            }

            return new RouteResult(false, null, "Modulo non riconosciuto");
        }
        catch (Exception ex)
        {
            return new RouteResult(false, null, ex.Message);
        }
    }
}