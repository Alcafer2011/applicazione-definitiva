using DigitalBrain.Service.Models;

namespace DigitalBrain.Service.Abstractions;

public interface IMemoryStore
{
    Task AddAsync(MemoryRecord record, CancellationToken ct = default);
    Task<IReadOnlyList<MemoryRecord>> QueryAsync(string tenantId, string? type, CancellationToken ct = default);
}