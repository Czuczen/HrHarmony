using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Address;
using HrHarmony.Data.Repositories.Dto;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;

namespace HrHarmony.Controllers;

public class AddressController : Controller
{
    private readonly IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> _addressRepository;
    private readonly IMapper _mapper;

    public AddressController(
        IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> addressRepository,
        IMapper mapper
    )
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(PaginationRequest paginationRequest)
    {
        var pagedEntities = await _addressRepository.GetPagedEntitiesAsCustomObjectAsync<IndexViewModel>(paginationRequest);
        return View(_mapper.Map<PagedRecordsViewModel<IndexViewModel>>(pagedEntities));
    }

    public async Task<IActionResult> Details(int id)
    {
        var entity = await _addressRepository.GetByIdWithRelatedAsCustomObjectAsync<DetailsViewModel>(id);
        if (entity == null)
            return NotFound();

        entity.IsMainView = true;

        return View(entity);
    }

    public async Task<IActionResult> Create()
    {
        var createViewModel = new CreateViewModel();

        return View(createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddressCreateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _addressRepository.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        var createViewModel = _mapper.Map<CreateViewModel>(entity);

        return View(createViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var updateViewModel = await _addressRepository.GetByIdAsCustomObjectAsync<UpdateViewModel>(id);
        if (updateViewModel == null)
            return NotFound();

        return View(updateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AddressUpdateDto entity)
    {
        if (ModelState.IsValid)
        {
            await _addressRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        var updateViewModel = _mapper.Map<UpdateViewModel>(entity);

        return View(updateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _addressRepository.GetByIdAsCustomObjectAsync<DeleteViewModel>(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _addressRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}