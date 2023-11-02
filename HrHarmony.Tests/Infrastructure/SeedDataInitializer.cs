using HrHarmony.Tests.Configuration;

namespace HrHarmony.Tests.Infrastructure;

public class SeedDataInitializer : HrHarmonyTestsBase<object>
{
    public SeedDataInitializer(TestFixture fixture) : base(fixture, true)
    {
    }

    [Fact]
    public void Init()
    {
    }
}
