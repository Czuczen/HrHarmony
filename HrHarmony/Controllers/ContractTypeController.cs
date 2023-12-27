using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.ViewModels.ContractType;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Dictionary;

namespace HrHarmony.Controllers;

public class ContractTypeController : Controller
{
    private readonly IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> _contractTypeRepository;
    private readonly IMapper _mapper;

    public ContractTypeController(
        IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> contractTypeRepository,
        IMapper mapper
    )
    {
        _contractTypeRepository = contractTypeRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _contractTypeRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _contractTypeRepository.GetByIdAsCustomObjectAsync<DetailsViewModel>(id));
    }

    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContractTypeCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _contractTypeRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _contractTypeRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ContractTypeUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _contractTypeRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View(await _contractTypeRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _contractTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}