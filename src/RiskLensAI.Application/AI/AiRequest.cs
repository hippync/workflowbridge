namespace RiskLensAI.Application.AI;

public sealed record AiRequest(
    IReadOnlyList<AiMessage> Messages,
    string? Model = null,
    int? MaxTokens = null,
    string? SystemPrompt = null);
