namespace CryptoProspector.Tests.Configuration;

public abstract class CryptoProspectorTestsBase<TService> : IClassFixture<TestFixture>
    where TService : class
{
    protected readonly TService Service;

    public CryptoProspectorTestsBase(TestFixture fixture)
    {
        Service = fixture.ServiceProvider.GetService(typeof(TService)) as TService;
    }
}