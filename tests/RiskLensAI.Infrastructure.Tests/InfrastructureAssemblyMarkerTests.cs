using RiskLensAI.Infrastructure;

namespace RiskLensAI.Infrastructure.Tests;

public class InfrastructureAssemblyMarkerTests
{
    [Fact]
    public void InfrastructureAssemblyMarker_IsAvailable()
    {
        Assert.Equal("RiskLensAI.Infrastructure", typeof(InfrastructureAssemblyMarker).Namespace);
    }
}
