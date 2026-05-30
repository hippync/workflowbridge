using RiskLensAI.Application;

namespace RiskLensAI.Application.Tests;

public class ApplicationAssemblyMarkerTests
{
    [Fact]
    public void ApplicationAssemblyMarker_IsAvailable()
    {
        Assert.Equal("RiskLensAI.Application", typeof(ApplicationAssemblyMarker).Namespace);
    }
}
