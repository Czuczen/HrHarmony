using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.Address;

namespace HrHarmony.Controllers;

public class AddressController : Controller
{
    private readonly ILogger<AddressController> _logger; // dodać obsługę wyjątków
    private readonly IMapper _mapper;
    private readonly IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> _addressRepository;

    public AddressController(
        IRepository<Address, int, AddressDto, AddressUpdateDto, AddressCreateDto> addressRepository,
        ILogger<AddressController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<IActionResult> Index()
    {
        var addresses = await _addressRepository.GetAll();
        var mappedAddresses = _mapper.Map<IEnumerable<IndexViewModel>>(addresses);

        return View(mappedAddresses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var address = await _addressRepository.GetById(id);
        if (address == null)
            return NotFound();

        var mappedAddress = _mapper.Map<DetailsViewModel>(address);
        mappedAddress.IsMainView = true;

        return View(mappedAddress);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddressCreateDto address)
    {
        if (ModelState.IsValid)
        {
            await _addressRepository.Create(address);

            return RedirectToAction("Index");
        }

        var mappedAddress = _mapper.Map<CreateViewModel>(address);

        return View(mappedAddress);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var address = await _addressRepository.GetById(id);
        if (address == null)
            return NotFound();

        var mappedAddress = _mapper.Map<UpdateViewModel>(address);

        return View(mappedAddress);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AddressUpdateDto address)
    {
        if (ModelState.IsValid)
        {
            await _addressRepository.Update(address);
            return RedirectToAction("Index");
        }

        var mappedAddress = _mapper.Map<UpdateViewModel>(address);

        return View(mappedAddress);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var address = await _addressRepository.GetById(id);
        if (address == null)
            return NotFound();

        var mappedAddress = _mapper.Map<DeleteViewModel>(address);

        return View(mappedAddress);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _addressRepository.Delete(id);

        return RedirectToAction("Index");
    }
}