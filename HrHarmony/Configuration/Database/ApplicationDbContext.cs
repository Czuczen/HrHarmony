using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Configuration.Database;

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



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /// TODO: Jak zdąże to ogarnąć to i zrobić lazy loading!!!!!!!!!!!!

        // Employee
        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.MaritalStatus)
        //    .WithMany()
        //    .HasForeignKey(e => e.MaritalStatusId);
        //modelBuilder.Entity<Employee>().Navigation(e => e.MaritalStatus).AutoInclude();

        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.Address)
        //    .WithMany()
        //    .HasForeignKey(e => e.AddressId);
        //modelBuilder.Entity<Employee>().Navigation(e => e.Address).AutoInclude();

        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.EducationLevel)
        //    .WithMany()
        //    .HasForeignKey(e => e.EducationLevelId);
        //modelBuilder.Entity<Employee>().Navigation(e => e.EducationLevel).AutoInclude();

        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.Experience)
        //    .WithMany()
        //    .HasForeignKey(e => e.ExperienceId);
        //modelBuilder.Entity<Employee>().Navigation(e => e.Experience).AutoInclude();




        //modelBuilder.Entity<Employee>().Navigation(e => e.Contracts).AutoInclude();
        //modelBuilder.Entity<Employee>().Navigation(e => e.Leaves).AutoInclude();
        //modelBuilder.Entity<Employee>().Navigation(e => e.Absences).AutoInclude();


        // =========================================================================================


        // Absence
        //modelBuilder.Entity<Absence>()
        //   .HasOne(e => e.AbsenceType)
        //   .WithMany()
        //   .HasForeignKey(e => e.AbsenceTypeId);

        //modelBuilder.Entity<Absence>()
        //   .HasOne(e => e.Employee)
        //   .WithMany()
        //   .HasForeignKey(e => e.EmployeeId);

        // EmploymentContract
        //modelBuilder.Entity<EmploymentContract>()
        //    .HasOne(e => e.ContractType)
        //    .WithMany()
        //    .HasForeignKey(e => e.ContractTypeId);

        //modelBuilder.Entity<EmploymentContract>()
        //    .HasOne(e => e.Employee)
        //    .WithMany()
        //    .HasForeignKey(e => e.EmployeeId);

        //// Leave
        //modelBuilder.Entity<Leave>()
        //    .HasOne(e => e.LeaveType)
        //    .WithMany()
        //    .HasForeignKey(e => e.LeaveTypeId);

        //modelBuilder.Entity<Leave>()
        //    .HasOne(e => e.Employee)
        //    .WithMany()
        //    .HasForeignKey(e => e.EmployeeId);
    }
}