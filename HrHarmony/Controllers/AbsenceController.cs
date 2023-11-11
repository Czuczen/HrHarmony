using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Absence;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Consts;
using HrHarmony.Models.Interfaces;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Controllers;

public class AbsenceController : Controller
{
    private readonly IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> _absenceRepository;
    private readonly IMapper _mapper;

    public AbsenceController(
        IRepository<Absence, int, AbsenceDto, AbsenceUpdateDto, AbsenceCreateDto> absenceRepository,
        IMapper mapper
    )
    {
        _absenceRepository = absenceRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _absenceRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        var entity = await _absenceRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
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
    public async Task<IActionResult> Create(AbsenceCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _absenceRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _absenceRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        if (updateViewModel == null)
            return NotFound();

        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AbsenceUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _absenceRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);
        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _absenceRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    private async Task LoadSelectOptions(IAbsenceOptionFields entity)
    {
        var absenceTypesQ = _absenceRepository.GetQuery<AbsenceType, CustomEntity<SelectListItem>>(q =>
         q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.AbsenceType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));

        var employeesQ = _absenceRepository.GetQuery<Employee, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } }));

        var results = await absenceTypesQ.Concat(employeesQ).ToListAsync();

        entity.AbsenceTypes = results.Where(c => c.EntityName == EntitiesNames.AbsenceType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}