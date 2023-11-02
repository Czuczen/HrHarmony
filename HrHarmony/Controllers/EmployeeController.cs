using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Models.ViewModels.Employee;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> _absenceRepository;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;
    private readonly IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> _maritalStatusRepository;
    private readonly IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> _addressRepository;
    private readonly IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> _educationLevelRepository;
    private readonly IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> _experienceRepository;
    private readonly IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> _employmentContractRepository;
    private readonly IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> _leaveRepository;
    
    public EmployeeController(
        IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> absenceRepository,
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> maritalStatusRepository,
        IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> addressRepository,
        IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> educationLevelRepository,
        IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> experienceRepository,
        IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> employmentContractRepository,
        IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> leaveRepository,
        ILogger<EmployeeController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;

        _absenceRepository = absenceRepository;
        _employeeRepository = employeeRepository;
        _maritalStatusRepository = maritalStatusRepository;
        _addressRepository = addressRepository;
        _educationLevelRepository = educationLevelRepository;
        _experienceRepository = experienceRepository;
        _employmentContractRepository = employmentContractRepository;
        _leaveRepository = leaveRepository;
    }

    public async Task<IActionResult> Index()
    {
        var GetById = _employeeRepository.GetById(1);
        var GetByIdAsync = await _employeeRepository.GetByIdAsync(1);

        var GetByIdWithRelated = _employeeRepository.GetByIdWithRelated(1);
        var GetByIdWithRelatedAsync = await _employeeRepository.GetByIdWithRelatedAsync(1);

        var GetWhere = _employeeRepository.GetWhere(item => item.Id == 1);
        var GetWhereAsync = await _employeeRepository.GetWhereAsync(item => item.Id == 1);

        var GetWhereWithRelated = _employeeRepository.GetWhereWithRelated(item => item.Id == 1);
        var GetWhereWithRelatedAsync = await _employeeRepository.GetWhereWithRelatedAsync(item => item.Id == 1);

        var GetQuerye = _employeeRepository.GetQuery(item => item.Id == 1).ToList();
        var GetQueryq = _employeeRepository.GetQuery(query => query.Where(entity => entity.Id == 1)).ToList();

        var GetQueryWithRelatede = _employeeRepository.GetQueryWithRelated(item => item.Id == 1).ToList();
        var GetQueryWithRelatedq = _employeeRepository.GetQueryWithRelated(query => query.Where(entity => entity.Id == 1)).ToList();

        var ExecuteQuery = _employeeRepository.ExecuteQuery(query => query.Where(entity => entity.Id == 1));
        var ExecuteQueryAsync = await _employeeRepository.ExecuteQueryAsync(query => query.Where(entity => entity.Id == 1));

        var ExecuteQueryWithRelated = _employeeRepository.ExecuteQueryWithRelated(query => query.Where(entity => entity.Id == 1));
        var ExecuteQueryWithRelatedAsync = await _employeeRepository.ExecuteQueryWithRelatedAsync(query => query.Where(entity => entity.Id == 1));

        var GetPagedEntities = _employeeRepository.GetPagedEntities(2, 10);
        var GetPagedEntitiesAsync = await _employeeRepository.GetPagedEntitiesAsync(2, 10);

        var GetPagedEntitiesWithRelated = _employeeRepository.GetPagedEntitiesWithRelated(2, 10);
        var GetPagedEntitiesWithRelatedAsync = await _employeeRepository.GetPagedEntitiesWithRelatedAsync(2, 10);

        var GetPagedEntitiesWithCustomFields = _employeeRepository.GetPagedEntitiesWithCustomFields(2, 10, query => query.Select(item => new EmployeeDto { Id = item.Id, FullName = item.FullName }));
        var GetPagedEntitiesWithCustomFieldsAsync = await _employeeRepository.GetPagedEntitiesWithCustomFieldsAsync(2, 10, query => query.Select(item => new EmployeeDto { Id = item.Id, FullName = item.FullName }));


        var GetEntityWithCustomFields = await _employeeRepository.GetEntityWithCustomFields(1, query => query.Select(item => new EmployeeDto { Id = item.Id, FullName = item.FullName }));
        

        var GetAll = _employeeRepository.GetAll();
        var GetAllAsync = await _employeeRepository.GetAllAsync();

        var GeAllWithRelated = _employeeRepository.GeAllWithRelated();
        var GetAllWithRelatedAsync = await _employeeRepository.GetAllWithRelatedAsync();

        
            


        //===================================================
        //===================================================


        var employees = await _employeeRepository.GetAllAsync();
        var mappedEmployees = _mapper.Map<IEnumerable<IndexViewModel>>(employees);

        return View(mappedEmployees);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        var mappedEmployee = _mapper.Map<DetailsViewModel>(employee);

        mappedEmployee.MaritalStatus = await _maritalStatusRepository.GetByIdAsync(mappedEmployee.MaritalStatusId);
        mappedEmployee.Address = await _addressRepository.GetByIdAsync(mappedEmployee.AddressId);
        mappedEmployee.EducationLevel = await _educationLevelRepository.GetByIdAsync(mappedEmployee.EducationLevelId);
        mappedEmployee.Experience = await _experienceRepository.GetByIdAsync(mappedEmployee.ExperienceId);

        mappedEmployee.Absences = _mapper.Map<IEnumerable<Models.ViewModels.Absence.DetailsViewModel>>(await _absenceRepository.GetWhere("EmployeeId", mappedEmployee.Id));
        mappedEmployee.Contracts = _mapper.Map<IEnumerable<Models.ViewModels.EmploymentContract.DetailsViewModel>>(await _employmentContractRepository.GetWhere("EmployeeId", mappedEmployee.Id));
        mappedEmployee.Leaves = _mapper.Map<IEnumerable<Models.ViewModels.Leave.DetailsViewModel>>(await _leaveRepository.GetWhere("EmployeeId", mappedEmployee.Id));

        mappedEmployee.IsMainView = true;

        return View(mappedEmployee);
    }

    public async Task<IActionResult> Create()
    {
        var employeeViewModel = new CreateViewModel();

        var allMaritalStatuses = await _maritalStatusRepository.GetAllAsync();
        employeeViewModel.MaritalStatuses = allMaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAllAsync();
        employeeViewModel.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAllAsync();
        employeeViewModel.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAllAsync();
        employeeViewModel.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });

        return View(employeeViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeCreateDto employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.Create(employee);
                
            return RedirectToAction("Index");
        }

        var mappedEmployee = _mapper.Map<CreateViewModel>(employee);

        var allMaritalStatuses = await _maritalStatusRepository.GetAllAsync();
        mappedEmployee.MaritalStatuses = allMaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAllAsync();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAllAsync();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAllAsync();
        mappedEmployee.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });

        return View(mappedEmployee);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        var mappedEmployee = _mapper.Map<UpdateViewModel>(employee);

        var allmaritalStatuses = await _maritalStatusRepository.GetAllAsync();
        mappedEmployee.MaritalStatuses = allmaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAllAsync();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAllAsync();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAllAsync();
        mappedEmployee.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });

        return View(mappedEmployee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeUpdateDto employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }

        var mappedEmployee = _mapper.Map<UpdateViewModel>(employee);

        var allmaritalStatuses = await _maritalStatusRepository.GetAllAsync();
        mappedEmployee.MaritalStatuses = allmaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAllAsync();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAllAsync();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAllAsync();
        mappedEmployee.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });


        return View(mappedEmployee);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        var mappedEmployee = _mapper.Map<DeleteViewModel>(employee);

        return View(mappedEmployee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeRepository.Delete(id);

        return RedirectToAction("Index");
    }
}