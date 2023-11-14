using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Consts;
using Microsoft.EntityFrameworkCore;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Models.Interfaces.SelectOptions;
using HrHarmony.Data.Models.ViewModels.Salary;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Main;

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
        return View(await _salaryRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id));
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
        return View(await _salaryRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
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
        return View(await _salaryRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
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

        entity.Employees = results.Where(c => c.EntityName == EntitiesNames.Employee).Select(e => e.Item);
    }
}