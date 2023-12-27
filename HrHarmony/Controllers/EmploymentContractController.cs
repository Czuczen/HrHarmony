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
using HrHarmony.Data.Models.ViewModels.EmploymentContract;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Main;

namespace HrHarmony.Controllers;

public class EmploymentContractController : Controller
{
    private readonly IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> _employmentContractRepository;
    private readonly IMapper _mapper;

    public EmploymentContractController(
        IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> employmentContractRepository,
        IMapper mapper
    )
    {
        _employmentContractRepository = employmentContractRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _employmentContractRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _employmentContractRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public async Task<IActionResult> Create()
    {
        var createViewModel = new CreateViewModel();
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmploymentContractCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _employmentContractRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _employmentContractRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmploymentContractUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _employmentContractRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);
        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _employmentContractRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employmentContractRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> SearchRelatedRecords(string searchTerm, string entityName) =>
        entityName switch
        {
            EntitiesNames.Employee => Json(await _employmentContractRepository.GetQuery<Employee, Employee>(q => q.Where(e =>
                        e.FullName.ToLower().Contains(searchTerm.ToLower()))).Select(e =>
                        new SelectListItem { Value = e.Id.ToString(), Text = e.FullName }).OrderBy(x => x.Text).ToListAsync()),

            _ => throw new InvalidOperationException($"Unsupported entity: '{entityName}'."),
        };

    private async Task LoadSelectOptions(ILoadGroupedEmploymentContractOptions entity)
    {
        var contractTypesQ = _employmentContractRepository.GetQuery<ContractType, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.ContractType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));

        var employeesQ = _employmentContractRepository.GetQuery<Employee, Employee>(q => q.Take(100))
            .Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

        // jeśli walidacja nie przeszła lub jest edycja to potrzebujemy wartości tekstowej dla pola wyszukiwania połączonych rekordów
        var query = contractTypesQ.Concat(employeesQ);
        if (entity.EmployeeId != 0)
        {
            var selectedEmployeeQ = _employmentContractRepository.GetQuery<Employee, Employee>(q => q.Where(e => e.Id == entity.EmployeeId))
                .Select(e => new CustomEntity<SelectListItem> { EntityName = "EmployeeText", Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } });

            query = query.Concat(selectedEmployeeQ);   
        }

        var results = await query.OrderBy(x => x.Item.Text).ToListAsync();

        entity.EmployeeText = results.Where(c => c.EntityName == "EmployeeText").SingleOrDefault()?.Item.Text;
        entity.ContractTypes = results.Where(c => c.EntityName == EntitiesNames.ContractType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}