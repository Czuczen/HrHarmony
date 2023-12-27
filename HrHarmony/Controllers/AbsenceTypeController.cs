using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.AbsenceType;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class AbsenceTypeController : Controller
{
    private readonly IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> _absenceTypeRepository;
    private readonly IMapper _mapper;

    public AbsenceTypeController(
        IRepository<AbsenceType, int, AbsenceTypeDto, AbsenceTypeUpdateDto, AbsenceTypeCreateDto> absenceTypeRepository,
        IMapper mapper
    )
    {
        _absenceTypeRepository = absenceTypeRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _absenceTypeRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _absenceTypeRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AbsenceTypeCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _absenceTypeRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _absenceTypeRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AbsenceTypeUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _absenceTypeRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _absenceTypeRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _absenceTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}