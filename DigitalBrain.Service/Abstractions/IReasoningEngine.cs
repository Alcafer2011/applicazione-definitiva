using DigitalBrain.Service.Models;

namespace DigitalBrain.Service.Abstractions;

public interface IReasoningEngine
{
    Task<ReasoningResult> ReasonAsync(ReasoningRequest request, CancellationToken ct = default);
}