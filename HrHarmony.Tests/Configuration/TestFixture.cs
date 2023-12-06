using HrHarmony.Configuration.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace HrHarmony.Tests.Configuration;

public class TestFixture
{
    public readonly IServiceProvider ServiceProvider;

    public TestFixture()
    {

#if DEBUG
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
#endif

        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterTestsDependencies();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}
