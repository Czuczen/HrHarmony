using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;

namespace HrHarmony.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;

    public EmployeeService(IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    // logika biznesowa
}