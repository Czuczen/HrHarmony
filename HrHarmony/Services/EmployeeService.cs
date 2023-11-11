using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Repositories.Entity;

namespace HrHarmony.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee, int> _employeeRepository;
    public readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IRepository<Employee, int> employeeRepository,
        ILogger<EmployeeService> logger
        )
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    // logika biznesowa
}