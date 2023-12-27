using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.MaritalStatus;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class MaritalStatusController : Controller
{
    private readonly IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> _maritalStatusRepository;
    private readonly IMapper _mapper;

    public MaritalStatusController(
        IRepository<MaritalStatus, int, MaritalStatusDto, MaritalStatusUpdateDto, MaritalStatusCreateDto> maritalStatusRepository,
        IMapper mapper
    )
    {
        _maritalStatusRepository = maritalStatusRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _maritalStatusRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _maritalStatusRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MaritalStatusCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _maritalStatusRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _maritalStatusRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MaritalStatusUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _maritalStatusRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _maritalStatusRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _maritalStatusRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}