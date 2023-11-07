using HrHarmony.Data.Database;
using HrHarmony.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HrHarmony.Tests.Configuration;

public abstract class HrHarmonyTestsBase<TService> : IClassFixture<TestFixture>
    where TService : class
{
    protected ApplicationDbContext Ctx;

    protected readonly TService Service;


    public HrHarmonyTestsBase(TestFixture fixture, bool initSeedData = false)
    {
        Ctx = new ApplicationDbContext(fixture.ServiceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        Service = fixture.ServiceProvider.GetService(typeof(TService)) as TService;

        if (initSeedData)
            SeedData.Initialize(Ctx);
    }
}