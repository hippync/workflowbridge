using RiskLensAI.Application.AI;

namespace RiskLensAI.Application.Tests.AI;

public class AiMessageTests
{
    [Fact]
    public void Constructor_SetsRoleAndContent()
    {
        var message = new AiMessage(AiMessageRole.User, "Hello");

        Assert.Equal(AiMessageRole.User, message.Role);
        Assert.Equal("Hello", message.Content);
    }

    [Theory]
    [InlineData(AiMessageRole.System)]
    [InlineData(AiMessageRole.User)]
    [InlineData(AiMessageRole.Assistant)]
    public void Constructor_AcceptsAllRoles(AiMessageRole role)
    {
        var message = new AiMessage(role, "content");

        Assert.Equal(role, message.Role);
    }

    [Fact]
    public void Equality_TrueForSameRoleAndContent()
    {
        var a = new AiMessage(AiMessageRole.User, "Hello");
        var b = new AiMessage(AiMessageRole.User, "Hello");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equality_FalseForDifferentContent()
    {
        var a = new AiMessage(AiMessageRole.User, "Hello");
        var b = new AiMessage(AiMessageRole.User, "World");

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void Equality_FalseForDifferentRole()
    {
        var a = new AiMessage(AiMessageRole.User, "Hello");
        var b = new AiMessage(AiMessageRole.Assistant, "Hello");

        Assert.NotEqual(a, b);
    }
}
