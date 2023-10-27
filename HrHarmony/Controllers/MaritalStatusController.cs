using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.MaritalStatus;

namespace HrHarmony.Controllers;

public class MaritalStatusController : Controller
{
    private readonly ILogger<MaritalStatusController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> _maritalStatusRepository;

    public MaritalStatusController(
        IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> maritalStatusRepository,
        ILogger<MaritalStatusController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _maritalStatusRepository = maritalStatusRepository;
    }

    public async Task<IActionResult> Index()
    {
        var maritalStatuses = await _maritalStatusRepository.GetAll();
        var mappedMaritalStatuses = _mapper.Map<IEnumerable<IndexViewModel>>(maritalStatuses);

        return View(mappedMaritalStatuses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var maritalStatus = await _maritalStatusRepository.GetById(id);
        if (maritalStatus == null)
            return NotFound();

        var mappedMaritalStatus = _mapper.Map<DetailsViewModel>(maritalStatus);
        mappedMaritalStatus.IsMainView = true;

        return View(mappedMaritalStatus);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MaritalStatusCreateDto maritalStatus)
    {
        if (ModelState.IsValid)
        {
            await _maritalStatusRepository.Create(maritalStatus);

            return RedirectToAction("Index");
        }

        var mappedMaritalStatus = _mapper.Map<CreateViewModel>(maritalStatus);

        return View(mappedMaritalStatus);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var maritalStatus = await _maritalStatusRepository.GetById(id);
        if (maritalStatus == null)
            return NotFound();

        var mappedMaritalStatus = _mapper.Map<UpdateViewModel>(maritalStatus);

        return View(mappedMaritalStatus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MaritalStatusUpdateDto maritalStatus)
    {
        if (ModelState.IsValid)
        {
            await _maritalStatusRepository.Update(maritalStatus);
            return RedirectToAction("Index");
        }

        var mappedMaritalStatus = _mapper.Map<UpdateViewModel>(maritalStatus);
        
        return View(mappedMaritalStatus);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var maritalStatus = await _maritalStatusRepository.GetById(id);
        if (maritalStatus == null)
            return NotFound();

        var mappedMaritalStatus = _mapper.Map<DeleteViewModel>(maritalStatus);

        return View(mappedMaritalStatus);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _maritalStatusRepository.Delete(id);

        return RedirectToAction("Index");
    }
}