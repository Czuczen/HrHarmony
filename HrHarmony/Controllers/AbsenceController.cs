using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Absence;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Create.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Controllers;

public class AbsenceController : Controller
{
    private readonly ILogger<AbsenceController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> _absenceRepository;
    private readonly IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> _absenceTypeRepository;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;

    public AbsenceController(
        IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> absenceRepository,
        IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> absenceTypeRepository,
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        ILogger<AbsenceController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _absenceRepository = absenceRepository;
        _absenceTypeRepository = absenceTypeRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var absences = await _absenceRepository.GetAll();
        var mappedAbsences = _mapper.Map<IEnumerable<IndexViewModel>>(absences);

        return View(mappedAbsences);
    }

    public async Task<IActionResult> Details(int id)
    {
        var absence = await _absenceRepository.GetById(id);
        if (absence == null)
            return NotFound();

        var mappedAbsence = _mapper.Map<DetailsViewModel>(absence);

        mappedAbsence.AbsenceType = await _absenceTypeRepository.GetById(mappedAbsence.AbsenceTypeId);
        mappedAbsence.Employee = await _employeeRepository.GetById(mappedAbsence.EmployeeId);

        mappedAbsence.IsMainView = true;

        return View(mappedAbsence);
    }

    public async Task<IActionResult> Create()
    {
        var absenceViewModel = new CreateViewModel();

        var allAbsenceTypes = await _absenceTypeRepository.GetAll();
        absenceViewModel.AbsenceTypes = allAbsenceTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        absenceViewModel.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(absenceViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AbsenceCreateDto absence)
    {
        if (ModelState.IsValid)
        {
            await _absenceRepository.Create(absence);

            return RedirectToAction("Index");
        }

        var mappedAbsence = _mapper.Map<CreateViewModel>(absence);

        var allAbsenceTypes = await _absenceTypeRepository.GetAll();
        mappedAbsence.AbsenceTypes = allAbsenceTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedAbsence.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedAbsence);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var absence = await _absenceRepository.GetById(id);
        if (absence == null)
            return NotFound();

        var mappedAbsence = _mapper.Map<UpdateViewModel>(absence);

        var allAbsenceTypes = await _absenceTypeRepository.GetAll();
        mappedAbsence.AbsenceTypes = allAbsenceTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedAbsence.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedAbsence);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AbsenceUpdateDto absence)
    {
        if (ModelState.IsValid)
        {
            await _absenceRepository.Update(absence);
            return RedirectToAction("Index");
        }

        var mappedAbsence = _mapper.Map<UpdateViewModel>(absence);

        var allAbsenceTypes = await _absenceTypeRepository.GetAll();
        mappedAbsence.AbsenceTypes = allAbsenceTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedAbsence.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedAbsence);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var absence = await _absenceRepository.GetById(id);
        if (absence == null)
            return NotFound();

        var mappedAbsence = _mapper.Map<DeleteViewModel>(absence);

        return View(mappedAbsence);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceRepository.Delete(id);

        return RedirectToAction("Index");
    }
}