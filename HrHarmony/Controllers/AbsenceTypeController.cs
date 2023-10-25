using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.AbsenceType;

namespace HrHarmony.Controllers;

public class AbsenceTypeController : Controller
{
    private readonly ILogger<AbsenceTypeController> _logger; // dodać obsługę wyjątków
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
        var absenceTypes = await _absenceTypeRepository.GetAll();
        var mappedAbsenceTypes = _mapper.Map<IEnumerable<IndexViewModel>>(absenceTypes);

        return View(mappedAbsenceTypes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var absenceType = await _absenceTypeRepository.GetById(id);
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
            await _absenceTypeRepository.Create(absenceType);

            return RedirectToAction("Index");
        }

        var mappedAbsenceType = _mapper.Map<CreateViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var absenceType = await _absenceTypeRepository.GetById(id);
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
            await _absenceTypeRepository.Update(absenceType);
            return RedirectToAction("Index");
        }

        var mappedAbsenceType = _mapper.Map<UpdateViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var absenceType = await _absenceTypeRepository.GetById(id);
        if (absenceType == null)
            return NotFound();

        var mappedAbsenceType = _mapper.Map<DeleteViewModel>(absenceType);

        return View(mappedAbsenceType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceTypeRepository.Delete(id);

        return RedirectToAction("Index");
    }
}