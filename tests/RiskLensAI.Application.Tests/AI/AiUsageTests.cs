using RiskLensAI.Application.AI;

namespace RiskLensAI.Application.Tests.AI;

public class AiUsageTests
{
    [Fact]
    public void Constructor_SetsInputAndOutputTokens()
    {
        var usage = new AiUsage(InputTokens: 100, OutputTokens: 50);

        Assert.Equal(100, usage.InputTokens);
        Assert.Equal(50, usage.OutputTokens);
    }

    [Fact]
    public void TotalTokens_IsSumOfInputAndOutput()
    {
        var usage = new AiUsage(InputTokens: 300, OutputTokens: 150);

        Assert.Equal(450, usage.TotalTokens);
    }

    [Fact]
    public void TotalTokens_IsZeroWhenBothAreZero()
    {
        var usage = new AiUsage(InputTokens: 0, OutputTokens: 0);

        Assert.Equal(0, usage.TotalTokens);
    }
}
