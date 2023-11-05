using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.ContractType;
using HrHarmony.Repositories.Crud;

namespace HrHarmony.Controllers;

public class ContractTypeController : Controller
{
    private readonly ILogger<ContractTypeController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> _contractTypeRepository;

    public ContractTypeController(
        IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> contractTypeRepository,
        ILogger<ContractTypeController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _contractTypeRepository = contractTypeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var contractTypes = await _contractTypeRepository.GetAllAsync();
        var mappedContractTypes = _mapper.Map<IEnumerable<IndexViewModel>>(contractTypes);

        return View(mappedContractTypes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var contractType = await _contractTypeRepository.GetByIdAsync(id);
        if (contractType == null)
            return NotFound();

        var mappedContractType = _mapper.Map<DetailsViewModel>(contractType);
        mappedContractType.IsMainView = true;

        return View(mappedContractType);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContractTypeCreateDto contractType)
    {
        if (ModelState.IsValid)
        {
            await _contractTypeRepository.CreateAsync(contractType);

            return RedirectToAction("Index");
        }

        var mappedContractType = _mapper.Map<CreateViewModel>(contractType);

        return View(mappedContractType);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var contractType = await _contractTypeRepository.GetByIdAsync(id);
        if (contractType == null)
            return NotFound();

        var mappedContractType = _mapper.Map<UpdateViewModel>(contractType);

        return View(mappedContractType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ContractTypeUpdateDto contractType)
    {
        if (ModelState.IsValid)
        {
            await _contractTypeRepository.UpdateAsync(contractType);
            return RedirectToAction("Index");
        }

        var mappedContractType = _mapper.Map<UpdateViewModel>(contractType);

        return View(mappedContractType);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var contractType = await _contractTypeRepository.GetByIdAsync(id);
        if (contractType == null)
            return NotFound();

        var mappedContractType = _mapper.Map<DeleteViewModel>(contractType);

        return View(mappedContractType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _contractTypeRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}