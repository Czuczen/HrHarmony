using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using Microsoft.EntityFrameworkCore;
using HrHarmony.Consts;
using HrHarmony.Data.Models.Interfaces;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Models.ViewModels.Employee;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Main;

namespace HrHarmony.Controllers;

public class EmployeeController : Controller
{
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeController(
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        IMapper mapper
    )
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _employeeRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _employeeRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public async Task<IActionResult> Create()
    {
        var createViewModel = new CreateViewModel();
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.CreateAsync(entity);
                
            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _employeeRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);
        await LoadSelectOptions(updateViewModel);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _employeeRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
    
    private async Task LoadSelectOptions(ILoadGroupedEmployeeOptions entity)
    {
        var maritalStatussesQ = _employeeRepository.GetQuery<MaritalStatus, CustomEntity<SelectListItem>>(q =>
         q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.MaritalStatus, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.StatusName } }));

        var addressesQ = _employeeRepository.GetQuery<Address, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Address, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.Street + " " + e.PostalCode + " " + e.City } }));

        var educationLevelsQ = _employeeRepository.GetQuery<EducationLevel, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.EducationLevel, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.LevelName } }));

        var experiancesQ = _employeeRepository.GetQuery<Experience, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Experience, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.ExperienceName } }));

        var results = await maritalStatussesQ.Concat(addressesQ).Concat(educationLevelsQ).Concat(experiancesQ).OrderBy(x => x.Item.Text).ToListAsync();

        entity.MaritalStatuses = results.Where(c => c.EntityName == EntitiesNames.MaritalStatus).Select(e => e.Item);
        entity.Addresses = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item);
        entity.EducationLevels = results.Where(c => c.EntityName == EntitiesNames.EducationLevel).Select(e => e.Item);
        entity.Experiences = results.Where(c => c.EntityName == EntitiesNames.Experience).Select(e => e.Item);
    }
}