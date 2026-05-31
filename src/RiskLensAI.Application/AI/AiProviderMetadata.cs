namespace RiskLensAI.Application.AI;

public sealed record AiProviderMetadata(
    string ProviderName,
    string? ModelId = null,
    long? LatencyMs = null,
    string? StopReason = null);
