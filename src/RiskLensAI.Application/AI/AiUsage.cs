namespace RiskLensAI.Application.AI;

public sealed record AiUsage(int InputTokens, int OutputTokens)
{
    public int TotalTokens => InputTokens + OutputTokens;
}
