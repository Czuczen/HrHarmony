using HrHarmony.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HrHarmony.Tests.Configuration;

public abstract class HrHarmonyTestsBase<TService> : IClassFixture<TestFixture>
    where TService : class
{
    protected readonly ApplicationDbContext Ctx;

    protected readonly TService Service;


    protected HrHarmonyTestsBase(TestFixture fixture)
    {
        Ctx = new ApplicationDbContext(fixture.ServiceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        Service = fixture.ServiceProvider.GetService(typeof(TService)) as TService;
    }
}