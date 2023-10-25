using HrHarmony.Configuration.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoProspector.Tests.Configuration;

public class TestFixture
{
    public readonly IServiceProvider ServiceProvider;

    public TestFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterTestsDependencies();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}