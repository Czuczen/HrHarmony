using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Models.ViewModels;
using HrHarmony.Models.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Models.Shared;
using HrHarmony.Data.Repositories.Dto;
using Microsoft.EntityFrameworkCore;
using HrHarmony.Consts;
using HrHarmony.Data.Repositories.Entity;

namespace HrHarmony.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;
    private readonly IRepository<Employee, int> _repository;

    public EmployeeController(
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        IRepository<Employee, int> repository,
        ILogger<EmployeeController> logger,
        IMapper mapper
    )
    {
        _employeeRepository = employeeRepository;
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _employeeRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);

        var pagedRecords = new PagedRecordsViewModel<IndexViewModel>
        {
            Items = pagedEntities.Items.ToList(),
            TotalCount = pagedEntities.TotalCount,
            SearchedCount = pagedEntities.SearchedCount,
            PageNumber = pagedEntities.PageNumber,
            PageSize = pagedEntities.PageSize,
            OrderBy = pagedEntities.OrderBy,
            IsDescending = pagedEntities.IsDescending,
            SearchString = pagedEntities.SearchString
        };

        return View(pagedRecords);
    }

    public async Task<IActionResult> Details(int id)
    {
        var entity = await _employeeRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
        if (entity == null)
            return NotFound();

        entity.IsMainView = true;

        return View(entity);
    }

    public async Task<IActionResult> Create()
    {
        var maritalStatussesQ = _employeeRepository.GetQuery<MaritalStatus, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.MaritalStatus, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.StatusName } } ));

        var addressesQ = _employeeRepository.GetQuery<Address, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Address, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.Street + " " + e.PostalCode + " " + e.City } } ));

        var educationLevelsQ = _employeeRepository.GetQuery<EducationLevel, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.EducationLevel, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.LevelName } } ));

        var experiancesQ = _employeeRepository.GetQuery<Experience, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Experience, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.ExperienceDescription } } ));

        var results = await maritalStatussesQ.Concat(addressesQ).Concat(educationLevelsQ).Concat(experiancesQ).ToListAsync();

        var createViewModel = new CreateViewModel 
        {
            MaritalStatuses = results.Where(c => c.EntityName == EntitiesNames.MaritalStatus).Select(e => e.Item),
            Addresses = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item),
            EducationLevels = results.Where(c => c.EntityName == EntitiesNames.EducationLevel).Select(e => e.Item),
            Experiences = results.Where(c => c.EntityName == EntitiesNames.Experience).Select(e => e.Item)
        };

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

        var maritalStatussesQ = _employeeRepository.GetQuery<MaritalStatus, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.MaritalStatus, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.StatusName } }));

        var addressesQ = _employeeRepository.GetQuery<Address, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Address, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.Street + " " + e.PostalCode + " " + e.City } }));

        var educationLevelsQ = _employeeRepository.GetQuery<EducationLevel, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.EducationLevel, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.LevelName } }));

        var experiancesQ = _employeeRepository.GetQuery<Experience, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Experience, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.ExperienceDescription } }));

        var results = await maritalStatussesQ.Concat(addressesQ).Concat(educationLevelsQ).Concat(experiancesQ).ToListAsync();

        createViewModel.MaritalStatuses = results.Where(c => c.EntityName == EntitiesNames.MaritalStatus).Select(e => e.Item);
        createViewModel.Addresses = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item);
        createViewModel.EducationLevels = results.Where(c => c.EntityName == EntitiesNames.EducationLevel).Select(e => e.Item);
        createViewModel.Experiences = results.Where(c => c.EntityName == EntitiesNames.Experience).Select(e => e.Item);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _employeeRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        if (entity == null)
            return NotFound();

        var maritalStatussesQ = _employeeRepository.GetQuery<MaritalStatus, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.MaritalStatus, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.StatusName } }));

        var addressesQ = _employeeRepository.GetQuery<Address, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Address, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.Street + " " + e.PostalCode + " " + e.City } }));

        var educationLevelsQ = _employeeRepository.GetQuery<EducationLevel, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.EducationLevel, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.LevelName } }));

        var experiancesQ = _employeeRepository.GetQuery<Experience, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Experience, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.ExperienceDescription } }));

        var results = await maritalStatussesQ.Concat(addressesQ).Concat(educationLevelsQ).Concat(experiancesQ).ToListAsync();

        entity.MaritalStatuses = results.Where(c => c.EntityName == EntitiesNames.MaritalStatus).Select(e => e.Item);
        entity.Addresses = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item);
        entity.EducationLevels = results.Where(c => c.EntityName == EntitiesNames.EducationLevel).Select(e => e.Item);
        entity.Experiences = results.Where(c => c.EntityName == EntitiesNames.Experience).Select(e => e.Item);

        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeUpdateDto employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.UpdateAsync(employee);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(employee);

        var maritalStatussesQ = _employeeRepository.GetQuery<MaritalStatus, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.MaritalStatus, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.StatusName } }));

        var addressesQ = _employeeRepository.GetQuery<Address, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Address, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.Street + " " + e.PostalCode + " " + e.City } }));

        var educationLevelsQ = _employeeRepository.GetQuery<EducationLevel, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.EducationLevel, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.LevelName } }));

        var experiancesQ = _employeeRepository.GetQuery<Experience, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Experience, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.ExperienceDescription } }));

        var results = await maritalStatussesQ.Concat(addressesQ).Concat(educationLevelsQ).Concat(experiancesQ).ToListAsync();

        updateViewModel.MaritalStatuses = results.Where(c => c.EntityName == EntitiesNames.MaritalStatus).Select(e => e.Item);
        updateViewModel.Addresses = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item);
        updateViewModel.EducationLevels = results.Where(c => c.EntityName == EntitiesNames.EducationLevel).Select(e => e.Item);
        updateViewModel.Experiences = results.Where(c => c.EntityName == EntitiesNames.Experience).Select(e => e.Item);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _employeeRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}