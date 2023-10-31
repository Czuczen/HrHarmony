using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories.EntityRepository;

namespace HrHarmony.Services;

public class EmployeeService : IEmployeeService, ITestEmloyee, ITororo
{
    private readonly IRepository<Employee, int> _employeeRepository;
    public readonly ILogger<EmployeeService> _logger;

    private Guid? _instanceGuid;
    public Guid InstanceGuid => (Guid)(_instanceGuid ?? (_instanceGuid = Guid.NewGuid()));


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