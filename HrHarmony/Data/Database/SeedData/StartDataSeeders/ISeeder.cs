using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders
{
    public interface ISeeder : ITransientDependency
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
