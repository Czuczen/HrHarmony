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
        var employees = await _employeeRepository.GetAll();
        var mappedEmployees = _mapper.Map<IEnumerable<IndexViewModel>>(employees);

        return View(mappedEmployees);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
            return NotFound();

        var mappedEmployee = _mapper.Map<DetailsViewModel>(employee);

        mappedEmployee.MaritalStatus = await _maritalStatusRepository.GetById(mappedEmployee.MaritalStatusId);
        mappedEmployee.Address = await _addressRepository.GetById(mappedEmployee.AddressId);
        mappedEmployee.EducationLevel = await _educationLevelRepository.GetById(mappedEmployee.EducationLevelId);
        mappedEmployee.Experience = await _experienceRepository.GetById(mappedEmployee.ExperienceId);

        mappedEmployee.Absences = _mapper.Map<IEnumerable<Models.ViewModels.Absence.DetailsViewModel>>(await _absenceRepository.GetWhere("EmployeeId", mappedEmployee.Id));
        mappedEmployee.Contracts = _mapper.Map<IEnumerable<Models.ViewModels.EmploymentContract.DetailsViewModel>>(await _employmentContractRepository.GetWhere("EmployeeId", mappedEmployee.Id));
        mappedEmployee.Leaves = _mapper.Map<IEnumerable<Models.ViewModels.Leave.DetailsViewModel>>(await _leaveRepository.GetWhere("EmployeeId", mappedEmployee.Id));

        mappedEmployee.IsMainView = true;

        return View(mappedEmployee);
    }

    public async Task<IActionResult> Create()
    {
        var employeeViewModel = new CreateViewModel();

        var allMaritalStatuses = await _maritalStatusRepository.GetAll();
        employeeViewModel.MaritalStatuses = allMaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAll();
        employeeViewModel.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAll();
        employeeViewModel.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAll();
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

        var allMaritalStatuses = await _maritalStatusRepository.GetAll();
        mappedEmployee.MaritalStatuses = allMaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAll();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAll();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAll();
        mappedEmployee.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });

        return View(mappedEmployee);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
            return NotFound();

        var mappedEmployee = _mapper.Map<UpdateViewModel>(employee);

        var allmaritalStatuses = await _maritalStatusRepository.GetAll();
        mappedEmployee.MaritalStatuses = allmaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAll();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAll();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAll();
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

        var allmaritalStatuses = await _maritalStatusRepository.GetAll();
        mappedEmployee.MaritalStatuses = allmaritalStatuses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.StatusName });

        var allAddresses = await _addressRepository.GetAll();
        mappedEmployee.Addresses = allAddresses.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Street + " " + item.PostalCode + " " + item.City });

        var allEducationLevels = await _educationLevelRepository.GetAll();
        mappedEmployee.EducationLevels = allEducationLevels.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.LevelName });

        var allExperiances = await _experienceRepository.GetAll();
        mappedEmployee.Experiences = allExperiances.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.ExperienceDescription });


        return View(mappedEmployee);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeRepository.GetById(id);
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