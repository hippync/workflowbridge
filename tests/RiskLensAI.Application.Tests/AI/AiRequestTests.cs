using RiskLensAI.Application.AI;

namespace RiskLensAI.Application.Tests.AI;

public class AiRequestTests
{
    [Fact]
    public void Constructor_SetsMessages()
    {
        var messages = new[] { new AiMessage(AiMessageRole.User, "Analyse this") };

        var request = new AiRequest(messages);

        Assert.Single(request.Messages);
        Assert.Equal("Analyse this", request.Messages[0].Content);
    }

    [Fact]
    public void Constructor_DefaultsModelToNull()
    {
        var request = new AiRequest([]);

        Assert.Null(request.Model);
    }

    [Fact]
    public void Constructor_DefaultsMaxTokensToNull()
    {
        var request = new AiRequest([]);

        Assert.Null(request.MaxTokens);
    }

    [Fact]
    public void Constructor_DefaultsSystemPromptToNull()
    {
        var request = new AiRequest([]);

        Assert.Null(request.SystemPrompt);
    }

    [Fact]
    public void Constructor_AcceptsOptionalFields()
    {
        var request = new AiRequest(
            Messages: [],
            Model: "claude-sonnet-4-6",
            MaxTokens: 1024,
            SystemPrompt: "You are a credit analyst assistant.");

        Assert.Equal("claude-sonnet-4-6", request.Model);
        Assert.Equal(1024, request.MaxTokens);
        Assert.Equal("You are a credit analyst assistant.", request.SystemPrompt);
    }
}
