using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Absence> Absences { get; set; }

    public DbSet<Employee> Employees { get; set; }
        
    public DbSet<EmploymentContract> EmploymentContracts { get; set; }

    public DbSet<Leave> Leaves { get; set; }

    public DbSet<Salary> Salaries { get; set; }




    public DbSet<AbsenceType> AbsenceTypes { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<ContractType> ContractTypes { get; set; }

    public DbSet<EducationLevel> EducationLevels { get; set; }

    public DbSet<Experience> Experiences { get; set; }

    public DbSet<LeaveType> LeaveTypes { get; set; }

    public DbSet<MaritalStatus> MaritalStatuses { get; set; }
}