using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.EducationLevel;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class EducationLevelController : Controller
{
    private readonly IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> _educationLevelRepository;
    private readonly IMapper _mapper;

    public EducationLevelController(
        IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> educationLevelRepository,
        IMapper mapper
    )
    {
        _educationLevelRepository = educationLevelRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _educationLevelRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _educationLevelRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EducationLevelCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _educationLevelRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _educationLevelRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EducationLevelUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _educationLevelRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _educationLevelRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _educationLevelRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}