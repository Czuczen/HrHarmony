using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Consts;
using Microsoft.EntityFrameworkCore;
using HrHarmony.Data.Models.Interfaces;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Models.ViewModels.Leave;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Main;

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
        return View(await _leaveRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id));
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
        return View(await _leaveRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
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
        return View(await _leaveRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> SearchRelatedRecords(string searchTerm, string entityName) =>
        entityName switch
        {
            EntitiesNames.Employee => Json(await _leaveRepository.GetQuery<Employee, Employee>(q => q.Where(e =>
                                e.FullName.ToLower().Contains(searchTerm.ToLower()))).Select(e =>
                                new SelectListItem { Value = e.Id.ToString(), Text = e.FullName }).ToListAsync()),

            _ => throw new InvalidOperationException($"Unsupported entity: '{entityName}'."),
        };

    private async Task LoadSelectOptions(ILoadGroupedLeaveOptions entity)
    {
        var leaveTypesQ = _leaveRepository.GetQuery<LeaveType, CustomEntity<SelectListItem>>(q =>
           q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.LeaveType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));

        var employeesQ = _leaveRepository.GetQuery<Employee, Employee>(q => q.Take(100))
            .Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

        // jeśli walidacja nie przeszła lub jest edycja to potrzebujemy wartości tekstowej dla pola wyszukiwania połączonych rekordów
        var query = leaveTypesQ.Concat(employeesQ);
        if (entity.EmployeeId != 0)
        {
            var selectedEmployeeQ = _leaveRepository.GetQuery<Employee, Employee>(q => q.Where(e => e.Id == entity.EmployeeId))
                .Select(e => new CustomEntity<SelectListItem> { EntityName = "EmployeeText", Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

            query = query.Concat(selectedEmployeeQ);
        }

        var results = await query.ToListAsync();

        entity.EmployeeText = results.Where(c => c.EntityName == "EmployeeText").SingleOrDefault()?.Item.Text;
        entity.LeaveTypes = results.Where(c => c.EntityName == EntitiesNames.LeaveType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}