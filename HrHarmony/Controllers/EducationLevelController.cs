using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.EducationLevel;

namespace HrHarmony.Controllers;

public class EducationLevelController : Controller
{
    private readonly ILogger<EducationLevelController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> _educationLevelRepository;

    public EducationLevelController(
        IRepository<EducationLevel, int, EducationLevelDto, EducationLevelUpdateDto, EducationLevelCreateDto> educationLevelRepository,
        ILogger<EducationLevelController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _educationLevelRepository = educationLevelRepository;
    }

    public async Task<IActionResult> Index()
    {
        var educationLevels = await _educationLevelRepository.GetAllAsync();
        var mappedEducationLevels = _mapper.Map<IEnumerable<IndexViewModel>>(educationLevels);

        return View(mappedEducationLevels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var educationLevel = await _educationLevelRepository.GetByIdAsync(id);
        if (educationLevel == null)
            return NotFound();

        var mappedEducationLevel = _mapper.Map<DetailsViewModel>(educationLevel);
        mappedEducationLevel.IsMainView = true;

        return View(mappedEducationLevel);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EducationLevelCreateDto educationLevel)
    {
        if (ModelState.IsValid)
        {
            await _educationLevelRepository.Create(educationLevel);

            return RedirectToAction("Index");
        }

        var mappedEducationLevel = _mapper.Map<CreateViewModel>(educationLevel);

        return View(mappedEducationLevel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var educationLevel = await _educationLevelRepository.GetByIdAsync(id);
        if (educationLevel == null)
            return NotFound();

        var mappedEducationLevel = _mapper.Map<UpdateViewModel>(educationLevel);

        return View(mappedEducationLevel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EducationLevelUpdateDto educationLevel)
    {
        if (ModelState.IsValid)
        {
            await _educationLevelRepository.Update(educationLevel);
            return RedirectToAction("Index");
        }

        var mappedEducationLevel = _mapper.Map<UpdateViewModel>(educationLevel);

        return View(mappedEducationLevel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var educationLevel = await _educationLevelRepository.GetByIdAsync(id);
        if (educationLevel == null)
            return NotFound();

        var mappedEducationLevel = _mapper.Map<DeleteViewModel>(educationLevel);

        return View(mappedEducationLevel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _educationLevelRepository.Delete(id);

        return RedirectToAction("Index");
    }
}