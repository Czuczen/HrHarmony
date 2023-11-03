using AutoMapper;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Salary;
using HrHarmony.Models.Dto.Create.Main;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Controllers;

public class SalaryController : Controller
{
    private readonly ILogger<SalaryController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Salary, int, SalaryDto, SalaryUpdateDto, SalaryCreateDto> _salaryRepository;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;

    public SalaryController(
        IRepository<Salary, int, SalaryDto, SalaryUpdateDto, SalaryCreateDto> salaryRepository,
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        ILogger<SalaryController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _salaryRepository = salaryRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var salaries = await _salaryRepository.GetAllAsync();
        var mappedSalaries = _mapper.Map<IEnumerable<IndexViewModel>>(salaries);

        return View(mappedSalaries);
    }

    public async Task<IActionResult> Details(int id)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        if (salary == null)
            return NotFound();

        var mappedSalary = _mapper.Map<DetailsViewModel>(salary);
        mappedSalary.Employee = await _employeeRepository.GetByIdAsync(mappedSalary.EmployeeId);
        mappedSalary.IsMainView = true;

        return View(mappedSalary);
    }

    public async Task<IActionResult> Create()
    {
        var salaryViewModel = new CreateViewModel();

        var allEmployees = await _employeeRepository.GetAllAsync();
        salaryViewModel.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(salaryViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SalaryCreateDto salary)
    {
        if (ModelState.IsValid)
        {
            await _salaryRepository.CreateAsync(salary);

            return RedirectToAction("Index");
        }

        var mappedSalary = _mapper.Map<CreateViewModel>(salary);

        var allEmployees = await _employeeRepository.GetAllAsync();
        mappedSalary.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedSalary);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        if (salary == null)
            return NotFound();

        var mappedSalary = _mapper.Map<UpdateViewModel>(salary);

        return View(mappedSalary);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SalaryUpdateDto salary)
    {
        if (ModelState.IsValid)
        {
            await _salaryRepository.UpdateAsync(salary);
            return RedirectToAction("Index");
        }

        var mappedSalary = _mapper.Map<UpdateViewModel>(salary);

        return View(mappedSalary);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        if (salary == null)
            return NotFound();

        var mappedSalary = _mapper.Map<DeleteViewModel>(salary);

        return View(mappedSalary);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _salaryRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}