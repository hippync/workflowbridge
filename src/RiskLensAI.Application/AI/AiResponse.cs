namespace RiskLensAI.Application.AI;

public sealed record AiResponse(
    string Content,
    AiUsage? Usage = null,
    AiProviderMetadata? Metadata = null);
