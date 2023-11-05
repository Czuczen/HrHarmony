using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.AbsenceType;
using HrHarmony.Repositories.Crud;

namespace HrHarmony.Controllers;

public class AbsenceTypeController : Controller
{
    private readonly ILogger<AbsenceTypeController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> _absenceTypeRepository;

    public AbsenceTypeController(
        IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> absenceTypeRepository,
        ILogger<AbsenceTypeController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _absenceTypeRepository = absenceTypeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var absenceTypes = await _absenceTypeRepository.GetAllAsync();
        var mappedAbsenceTypes = _mapper.Map<IEnumerable<IndexViewModel>>(absenceTypes);

        return View(mappedAbsenceTypes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var absenceType = await _absenceTypeRepository.GetByIdAsync(id);
        if (absenceType == null)
            return NotFound();

        var mappedAbsenceType = _mapper.Map<DetailsViewModel>(absenceType);
        mappedAbsenceType.IsMainView = true;

        return View(mappedAbsenceType);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AbsenceTypeCreateDto absenceType)
    {
        if (ModelState.IsValid)
        {
            await _absenceTypeRepository.CreateAsync(absenceType);

            return RedirectToAction("Index");
        }

        var mappedAbsenceType = _mapper.Map<CreateViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var absenceType = await _absenceTypeRepository.GetByIdAsync(id);
        if (absenceType == null)
            return NotFound();

        var mappedAbsenceType = _mapper.Map<UpdateViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AbsenceTypeUpdateDto absenceType)
    {
        if (ModelState.IsValid)
        {
            await _absenceTypeRepository.UpdateAsync(absenceType);
            return RedirectToAction("Index");
        }

        var mappedAbsenceType = _mapper.Map<UpdateViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var absenceType = await _absenceTypeRepository.GetByIdAsync(id);
        if (absenceType == null)
            return NotFound();

        var mappedAbsenceType = _mapper.Map<DeleteViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}