using RiskLensAI.Application.AI;

namespace RiskLensAI.Application.Tests.AI;

public class AiResponseTests
{
    [Fact]
    public void Constructor_SetsContent()
    {
        var response = new AiResponse("Risk rating: Low");

        Assert.Equal("Risk rating: Low", response.Content);
    }

    [Fact]
    public void Constructor_DefaultsUsageToNull()
    {
        var response = new AiResponse("content");

        Assert.Null(response.Usage);
    }

    [Fact]
    public void Constructor_DefaultsMetadataToNull()
    {
        var response = new AiResponse("content");

        Assert.Null(response.Metadata);
    }

    [Fact]
    public void Constructor_AcceptsUsageAndMetadata()
    {
        var usage = new AiUsage(InputTokens: 200, OutputTokens: 100);
        var metadata = new AiProviderMetadata(
            ProviderName: "Mock",
            ModelId: "mock-v1",
            LatencyMs: 12,
            StopReason: "end_turn");

        var response = new AiResponse("content", usage, metadata);

        Assert.Equal(200, response.Usage!.InputTokens);
        Assert.Equal("Mock", response.Metadata!.ProviderName);
        Assert.Equal("end_turn", response.Metadata.StopReason);
    }

    [Fact]
    public void Constructor_AcceptsUsageWithoutMetadata()
    {
        var usage = new AiUsage(100, 50);
        var response = new AiResponse("content", Usage: usage);

        Assert.NotNull(response.Usage);
        Assert.Null(response.Metadata);
    }
}
