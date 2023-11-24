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
using HrHarmony.Data.Models.ViewModels.Absence;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Main;

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
        return View(await _absenceRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id));
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
        return View(await _absenceRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    /// TODO: Zrobić paginacje. Na końcu listy strzałka do kliknięcia żeby pobrała się kolejna strona?
    public async Task<IActionResult> SearchRelatedRecords(string searchTerm, string entityName) =>
        entityName switch 
        {
            EntitiesNames.Employee => Json(await _absenceRepository.GetQuery<Employee, Employee>(q => q.Where(e =>
                                e.FullName.ToLower().Contains(searchTerm.ToLower()))).Select(e =>
                                new SelectListItem { Value = e.Id.ToString(), Text = e.FullName }).ToListAsync()),
            
            _ => throw new InvalidOperationException($"Unsupported entity: '{entityName}'."),
        };
    
    private async Task LoadSelectOptions(ILoadGroupedAbsenceOptions entity)
    {
        var absenceTypesQ = _absenceRepository.GetQuery<AbsenceType, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.AbsenceType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));

        var employeesQ = _absenceRepository.GetQuery<Employee, Employee>(q => q.Take(100))
            .Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

        // jeśli walidacja nie przeszła lub jest edycja to potrzebujemy wartości tekstowej dla pola wyszukiwania połączonych rekordów
        var query = absenceTypesQ.Concat(employeesQ);
        if (entity.EmployeeId != 0)
        {
            var selectedEmployeeQ = _absenceRepository.GetQuery<Employee, Employee>(q => q.Where(e => e.Id == entity.EmployeeId))
                .Select(e => new CustomEntity<SelectListItem> { EntityName = "EmployeeText", Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

            query = query.Concat(selectedEmployeeQ);
        }

        var results = await query.ToListAsync();

        entity.EmployeeText = results.Where(c => c.EntityName == "EmployeeText").SingleOrDefault()?.Item.Text;
        entity.AbsenceTypes = results.Where(c => c.EntityName == EntitiesNames.AbsenceType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}