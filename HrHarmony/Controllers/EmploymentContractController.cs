using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.EmploymentContract;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Consts;
using HrHarmony.Models.Interfaces;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
        var entity = await _employmentContractRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
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
        if (updateViewModel == null)
            return NotFound();

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
        var entity = await _employmentContractRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employmentContractRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    private async Task LoadSelectOptions(IEmploymentContractOptionFields entity)
    {
        var contractTypesQ = _employmentContractRepository.GetQuery<ContractType, CustomEntity<SelectListItem>>(q =>
         q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.ContractType, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.TypeName } }));
        
        var employeesQ = _employmentContractRepository.GetQuery<Employee, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } }));

        var results = await contractTypesQ.Concat(employeesQ).ToListAsync();

        entity.ContractTypes = results.Where(c => c.EntityName == EntitiesNames.ContractType).Select(e => e.Item);
        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}