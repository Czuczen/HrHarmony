using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.LeaveType;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class LeaveTypeController : Controller
{
    private readonly IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> _leaveTypeRepository;
    private readonly IMapper _mapper;

    public LeaveTypeController(
        IRepository<LeaveType, int, LeaveTypeDto, LeaveTypeUpdateDto, LeaveTypeCreateDto> leaveTypeRepository,
        IMapper mapper
    )
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _leaveTypeRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _leaveTypeRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _leaveTypeRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _leaveTypeRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LeaveTypeUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _leaveTypeRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _leaveTypeRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}