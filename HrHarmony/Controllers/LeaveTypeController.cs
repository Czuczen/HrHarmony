using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.LeaveType;
using HrHarmony.Data.Repositories.Dto;

namespace HrHarmony.Controllers;

public class LeaveTypeController : Controller
{
    private readonly ILogger<LeaveTypeController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> _leaveTypeRepository;

    public LeaveTypeController(
        IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> leaveTypeRepository,
        ILogger<LeaveTypeController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var leaveTypes = await _leaveTypeRepository.GetAllAsync();
        var mappedLeaveTypes = _mapper.Map<IEnumerable<IndexViewModel>>(leaveTypes);

        return View(mappedLeaveTypes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        if (leaveType == null)
            return NotFound();

        var mappedLeaveType = _mapper.Map<DetailsViewModel>(leaveType);
        mappedLeaveType.IsMainView = true;

        return View(mappedLeaveType);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeCreateDto leaveType)
    {
        if (ModelState.IsValid)
        {
            await _leaveTypeRepository.CreateAsync(leaveType);

            return RedirectToAction("Index");
        }

        var mappedLeaveType = _mapper.Map<CreateViewModel>(leaveType);

        return View(mappedLeaveType);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        if (leaveType == null)
            return NotFound();

        var mappedLeaveType = _mapper.Map<UpdateViewModel>(leaveType);

        return View(mappedLeaveType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LeaveTypeUpdateDto leaveType)
    {
        if (ModelState.IsValid)
        {
            await _leaveTypeRepository.UpdateAsync(leaveType);
            return RedirectToAction("Index");
        }

        var mappedLeaveType = _mapper.Map<UpdateViewModel>(leaveType);

        return View(mappedLeaveType);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        if (leaveType == null)
            return NotFound();

        var mappedLeaveType = _mapper.Map<DeleteViewModel>(leaveType);

        return View(mappedLeaveType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

}