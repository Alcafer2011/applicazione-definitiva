using System.Collections.Generic;

namespace DigitalBrain.Service.Models;

public record ReasoningRequest(
    string TenantId,
    string Question,
    IReadOnlyList<MemoryRecord> Context
);

public record ReasoningResult(
    string Answer,
    IReadOnlyList<string> EvidenceIds
);