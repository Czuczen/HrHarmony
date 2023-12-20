using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class LeaveDataSeeder : ISeeder
{
    public int Order => 11;

    private readonly List<Leave> _leaves = new()
    {
        new Leave
        {
            Id = 1,
            StartDate = new DateTime(2023, 1, 10),
            EndDate = new DateTime(2023, 1, 15),
            LeaveTypeId = 1,
            EmployeeId = 1
        },
        new Leave
        {
            Id = 2,
            StartDate = new DateTime(2023, 2, 20),
            EndDate = new DateTime(2023, 2, 25),
            LeaveTypeId = 2,
            EmployeeId = 2
        },
        new Leave
        {
            Id = 3,
            StartDate = new DateTime(2023, 3, 5),
            EndDate = new DateTime(2023, 3, 10),
            LeaveTypeId = 3,
            EmployeeId = 3
        },
        new Leave
        {
            Id = 4,
            StartDate = new DateTime(2023, 4, 15),
            EndDate = new DateTime(2023, 4, 20),
            LeaveTypeId = 4,
            EmployeeId = 4
        },
        new Leave
        {
            Id = 5,
            StartDate = new DateTime(2023, 5, 25),
            EndDate = new DateTime(2023, 5, 30),
            LeaveTypeId = 5,
            EmployeeId = 5
        },
        new Leave
        {
            Id = 6,
            StartDate = new DateTime(2023, 6, 10),
            EndDate = new DateTime(2023, 6, 15),
            LeaveTypeId = 6,
            EmployeeId = 1
        },
        new Leave
        {
            Id = 7,
            StartDate = new DateTime(2023, 9, 1),
            EndDate = new DateTime(2023, 9, 5),
            LeaveTypeId = 9,
            EmployeeId = 2
        },
        new Leave
        {
            Id = 8,
            StartDate = new DateTime(2023, 10, 10),
            EndDate = new DateTime(2023, 10, 15),
            LeaveTypeId = 10,
            EmployeeId = 3
        },
        new Leave
        {
            Id = 9,
            StartDate = new DateTime(2023, 11, 20),
            EndDate = new DateTime(2023, 11, 25),
            LeaveTypeId = 11,
            EmployeeId = 4
        },
        new Leave
        {
            Id = 10,
            StartDate = new DateTime(2023, 12, 5),
            EndDate = new DateTime(2023, 12, 10),
            LeaveTypeId = 1,
            EmployeeId = 5
        },
        new Leave
        {
            Id = 11,
            StartDate = new DateTime(2024, 1, 15),
            EndDate = new DateTime(2024, 1, 20),
            LeaveTypeId = 2,
            EmployeeId = 1
        },
        new Leave
        {
            Id = 12,
            StartDate = new DateTime(2024, 2, 25),
            EndDate = new DateTime(2024, 2, 29),
            LeaveTypeId = 3,
            EmployeeId = 2
        },
        new Leave
        {
            Id = 13,
            StartDate = new DateTime(2024, 3, 5),
            EndDate = new DateTime(2024, 3, 10),
            LeaveTypeId = 4,
            EmployeeId = 3
        },
        new Leave
        {
            Id = 14,
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20),
            LeaveTypeId = 5,
            EmployeeId = 4
        },
        new Leave
        {
            Id = 15,
            StartDate = new DateTime(2024, 5, 25),
            EndDate = new DateTime(2024, 5, 30),
            LeaveTypeId = 6,
            EmployeeId = 5
        }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var leave in _leaves)
            modelBuilder.Entity<Leave>().HasData(leave);
    }
}