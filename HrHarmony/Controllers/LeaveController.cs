using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Leave;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;
using HrHarmony.Consts;
using HrHarmony.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Controllers;

public class LeaveController : Controller
{
    private readonly IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> _leaveRepository;
    private readonly IMapper _mapper;

    public LeaveController(
        IRepository<Leave, int, LeaveDto, LeaveUpdateDto, LeaveCreateDto> leaveRepository,
        IMapper mapper
    )
    {
        _leaveRepository = leaveRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _leaveRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        var entity = await _leaveRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
        if (entity == null)
            return NotFound();

        entity.IsMainView = true;

        return View(entity);
    }

    public async Task<IActionResult> Create()
    {
        var createViewModel = new CreateViewModel();
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _leaveRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _leaveRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        if (updateViewModel == null)
            return NotFound();

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LeaveUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _leaveRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _leaveRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    private async Task LoadSelectOptions(ILeaveOptionFields entity)
    {
        var leaveTypesQ = _leaveRepository.GetQuery<LeaveType, CustomEntity<SelectListItem>>(q =>
           q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.LeaveType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));

        var employeesQ = _leaveRepository.GetQuery<Employee, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } }));

        var results = await leaveTypesQ.Concat(employeesQ).ToListAsync();

        entity.LeaveTypes = results.Where(c => c.EntityName == EntitiesNames.LeaveType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}