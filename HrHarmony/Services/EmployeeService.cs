using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories.EntityRepository;

namespace HrHarmony.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee, int> _employeeRepository;

    public EmployeeService(IRepository<Employee, int> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    // logika biznesowa
}