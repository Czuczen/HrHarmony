namespace HrHarmony.Tests.Configuration;

public abstract class HrHarmonyTestsBase<TService> : IClassFixture<TestFixture>
    where TService : class
{
    protected readonly TService Service;

    public HrHarmonyTestsBase(TestFixture fixture)
    {
        Service = fixture.ServiceProvider.GetService(typeof(TService)) as TService;
    }
}