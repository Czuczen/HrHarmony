using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Leave;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Create.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Controllers;

public class LeaveController : Controller
{
    private readonly ILogger<LeaveController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> _leaveRepository;
    private readonly IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> _leaveTypeRepository;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;

    public LeaveController(
        IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> leaveRepository,
        IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> leaveTypeRepository,
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        ILogger<LeaveController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _leaveRepository = leaveRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var leaves = await _leaveRepository.GetAllAsync();
        var mappedLeaves = _mapper.Map<IEnumerable<IndexViewModel>>(leaves);

        return View(mappedLeaves);
    }

    public async Task<IActionResult> Details(int id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            return NotFound();

        var mappedLeave = _mapper.Map<DetailsViewModel>(leave);

        mappedLeave.LeaveType = await _leaveTypeRepository.GetByIdAsync(mappedLeave.LeaveTypeId);
        mappedLeave.Employee = await _employeeRepository.GetByIdAsync(mappedLeave.EmployeeId);

        mappedLeave.IsMainView = true;

        return View(mappedLeave);
    }

    public async Task<IActionResult> Create()
    {
        var leaveViewModel = new CreateViewModel();

        var allLeaveTypes = await _leaveTypeRepository.GetAllAsync();
        leaveViewModel.LeaveTypes = allLeaveTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAllAsync();
        leaveViewModel.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(leaveViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveCreateDto leave)
    {
        if (ModelState.IsValid)
        {
            await _leaveRepository.Create(leave);

            return RedirectToAction("Index");
        }

        var mappedLeave = _mapper.Map<CreateViewModel>(leave);

        var allLeaveTypes = await _leaveTypeRepository.GetAllAsync();
        mappedLeave.LeaveTypes = allLeaveTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAllAsync();
        mappedLeave.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedLeave);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            return NotFound();

        var mappedLeave = _mapper.Map<UpdateViewModel>(leave);

        return View(mappedLeave);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LeaveUpdateDto leave)
    {
        if (ModelState.IsValid)
        {
            await _leaveRepository.Update(leave);
            return RedirectToAction("Index");
        }

        var mappedLeave = _mapper.Map<UpdateViewModel>(leave);

        return View(mappedLeave);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            return NotFound();

        var mappedLeave = _mapper.Map<DeleteViewModel>(leave);

        return View(mappedLeave);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveRepository.Delete(id);

        return RedirectToAction("Index");
    }
}