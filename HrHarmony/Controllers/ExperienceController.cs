using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Experience;
using HrHarmony.Repositories.Crud;

namespace HrHarmony.Controllers;

public class ExperienceController : Controller
{
    private readonly ILogger<ExperienceController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> _experienceRepository;

    public ExperienceController(
        IRepository<Experience, int, ExperienceDto, ExperienceUpdateDto, ExperienceCreateDto> experienceRepository,
        ILogger<ExperienceController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _experienceRepository = experienceRepository;
    }

    public async Task<IActionResult> Index()
    {
        var experiences = await _experienceRepository.GetAllAsync();
        var mappedExperiences = _mapper.Map<IEnumerable<IndexViewModel>>(experiences);

        return View(mappedExperiences);
    }

    public async Task<IActionResult> Details(int id)
    {
        var experience = await _experienceRepository.GetByIdAsync(id);
        if (experience == null)
            return NotFound();

        var mappedExperience = _mapper.Map<DetailsViewModel>(experience);
        mappedExperience.IsMainView = true;

        return View(mappedExperience);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExperienceCreateDto experience)
    {
        if (ModelState.IsValid)
        {
            await _experienceRepository.CreateAsync(experience);

            return RedirectToAction("Index");
        }

        var mappedExperience = _mapper.Map<CreateViewModel>(experience);

        return View(mappedExperience);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var experience = await _experienceRepository.GetByIdAsync(id);
        if (experience == null)
            return NotFound();

        var mappedExperience = _mapper.Map<UpdateViewModel>(experience);

        return View(mappedExperience);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ExperienceUpdateDto experience)
    {
        if (ModelState.IsValid)
        {
            await _experienceRepository.UpdateAsync(experience);
            return RedirectToAction("Index");
        }

        var mappedExperience = _mapper.Map<UpdateViewModel>(experience);

        return View(mappedExperience);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var experience = await _experienceRepository.GetByIdAsync(id);
        if (experience == null)
            return NotFound();

        var mappedExperience = _mapper.Map<DeleteViewModel>(experience);

        return View(mappedExperience);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _experienceRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}