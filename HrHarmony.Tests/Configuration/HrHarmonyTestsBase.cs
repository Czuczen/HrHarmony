using HrHarmony.Tests.Infrastructure;

namespace HrHarmony.Tests.Configuration;

public abstract class HrHarmonyTestsBase<TService> : IClassFixture<TestFixture>
    where TService : class
{
    protected readonly TService Service;

    public HrHarmonyTestsBase(TestFixture fixture, bool initSeedData = false)
    {
        Service = fixture.ServiceProvider.GetService(typeof(TService)) as TService;

        if (initSeedData)
            SeedData.Initialize(fixture.ServiceProvider);
    }
}