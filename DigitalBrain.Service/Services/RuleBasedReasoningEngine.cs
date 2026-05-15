using DigitalBrain.Service.Abstractions;
using DigitalBrain.Service.Models;

namespace DigitalBrain.Service.Services;

public class RuleBasedReasoningEngine : IReasoningEngine
{
    public Task<ReasoningResult> ReasonAsync(ReasoningRequest request, CancellationToken ct = default)
    {
        var recent = request.Context
            .OrderByDescending(x => x.CreatedAt)
            .Take(20)
            .ToList();

        var evidenceIds = recent.Select(x => x.Id).ToList();

        var lowerQ = request.Question.ToLowerInvariant();
        string answer;

        if (lowerQ.Contains("stato") || lowerQ.Contains("azienda"))
        {
            answer = "Riassunto stato azienda: " +
                     string.Join(" | ", recent.Select(x => x.Content));
        }
        else
        {
            answer = $"Ho analizzato {recent.Count} memorie recenti.";
        }

        return Task.FromResult(new ReasoningResult(answer, evidenceIds));
    }
}