using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.Experience;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class ExperienceController : Controller
{
    private readonly IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> _experienceRepository;
    private readonly IMapper _mapper;

    public ExperienceController(
        IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> experienceRepository,
        IMapper mapper
    )
    {
        _experienceRepository = experienceRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _experienceRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _experienceRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExperienceCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _experienceRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _experienceRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ExperienceUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _experienceRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }
        
        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _experienceRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _experienceRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}