using HrHarmony.Data.Database.Converters;
using HrHarmony.Data.Database.SeedData.StartDataSeeders;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Models.Entities.Management;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database;

public class ApplicationDbContext : DbContext
{
    private readonly IEnumerable<ISeeder>? _seeders;

    
    public DbSet<Absence> Absences { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<EmploymentContract> EmploymentContracts { get; set; }

    public DbSet<Salary> Salaries { get; set; }


    public DbSet<AbsenceType> AbsenceTypes { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<ContractType> ContractTypes { get; set; }

    public DbSet<EducationLevel> EducationLevels { get; set; }

    public DbSet<Experience> Experiences { get; set; }

    public DbSet<MaritalStatus> MaritalStatuses { get; set; }



    public DbSet<Visitor> Visitors { get; set; }



    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEnumerable<ISeeder>? seeders = null)
        : base(options)
    {
        _seeders = seeders?.OrderBy(seeder => seeder.Order);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dateOnlyToDateTimeConverter = new DateOnlyToDateTimeConverter();

        modelBuilder
            .Entity<EmploymentContract>()
            .Property(e => e.ContractSigningDate)
            .HasConversion(dateOnlyToDateTimeConverter);

        modelBuilder
            .Entity<EmploymentContract>()
            .Property(e => e.StartDate)
            .HasConversion(dateOnlyToDateTimeConverter);

        modelBuilder
            .Entity<EmploymentContract>()
            .Property(e => e.EndDate)
            .HasConversion(dateOnlyToDateTimeConverter);

        if (!base.Database.GetAppliedMigrations().Any() && _seeders != null)
            foreach (var seeder in _seeders)
                seeder.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
