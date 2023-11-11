using HrHarmony.Tests.Configuration;
using Microsoft.EntityFrameworkCore;

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

    [Fact]
    public void ClearAll()
    {
        var dbSets = Ctx.GetType().GetProperties().Where(p => 
            p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
        
        foreach (var prop in dbSets)
        {
            var objects = (IEnumerable<object>) prop.GetValue(Ctx)!;
            Ctx.RemoveRange(objects);
        }

        Ctx.SaveChanges();
    }
}
