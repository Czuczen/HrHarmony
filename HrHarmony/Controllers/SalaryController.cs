using AutoMapper;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Salary;
using HrHarmony.Models.Dto.Create.Main;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Consts;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using HrHarmony.Models.Interfaces.SelectOptions;

namespace HrHarmony.Controllers;

public class SalaryController : Controller
{
    private readonly IRepository<Salary, int, SalaryDto, SalaryUpdateDto, SalaryCreateDto> _salaryRepository;
    private readonly IMapper _mapper;

    public SalaryController(
        IRepository<Salary, int, SalaryDto, SalaryUpdateDto, SalaryCreateDto> salaryRepository,
        IMapper mapper
    )
    {
        _salaryRepository = salaryRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _salaryRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        var entity = await _salaryRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
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
    public async Task<IActionResult> Create(SalaryCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _salaryRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);
        await LoadSelectOptions(createViewModel);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _salaryRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        if (updateViewModel == null)
            return NotFound();

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SalaryUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _salaryRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _salaryRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _salaryRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }

    private async Task LoadSelectOptions(IEmployeeOptions entity)
    {
        var employeesQ = _salaryRepository.GetQuery<Employee, CustomEntity<SelectListItem>>(q =>
            q.Select(e => new CustomEntity<SelectListItem> { EntityName = EntitiesNames.Employee, Item = new SelectListItem { Value = e.Id.ToString(), Text = e.FullName } }));

        var results = await employeesQ.ToListAsync();

        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Address).Select(e => e.Item);
    }
}