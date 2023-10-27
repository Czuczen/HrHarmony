﻿using AutoMapper;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrHarmony.Models.ViewModels.EmploymentContract;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Create.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Controllers;

public class EmploymentContractController : Controller
{
    private readonly ILogger<EmploymentContractController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> _employmentContractRepository;
    private readonly IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> _contractTypeRepository;
    private readonly IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> _employeeRepository;

    public EmploymentContractController(
        IRepository<EmploymentContract, int, EmploymentContractDto, EmploymentContractUpdateDto, EmploymentContractCreateDto> employmentContractRepository,
        IRepository<ContractType, int, ContractTypeDto, ContractTypeUpdateDto, ContractTypeCreateDto> contractTypeRepository,
        IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto> employeeRepository,
        ILogger<EmploymentContractController> logger,
        IMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _employmentContractRepository = employmentContractRepository;
        _contractTypeRepository = contractTypeRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var employmentContracts = await _employmentContractRepository.GetAll();
        var mappedEmploymentContracts = _mapper.Map<IEnumerable<IndexViewModel>>(employmentContracts);

        return View(mappedEmploymentContracts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employmentContract = await _employmentContractRepository.GetById(id);
        if (employmentContract == null)
            return NotFound();

        var mappedEmploymentContract = _mapper.Map<DetailsViewModel>(employmentContract);

        mappedEmploymentContract.ContractType = await _contractTypeRepository.GetById(mappedEmploymentContract.ContractTypeId);
        mappedEmploymentContract.Employee = await _employeeRepository.GetById(mappedEmploymentContract.EmployeeId);

        mappedEmploymentContract.IsMainView = true;

        return View(mappedEmploymentContract);
    }

    public async Task<IActionResult> Create()
    {
        var employmentContractViewModel = new CreateViewModel();

        var allContractTypes = await _contractTypeRepository.GetAll();
        employmentContractViewModel.ContractTypes = allContractTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        employmentContractViewModel.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(employmentContractViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmploymentContractCreateDto employmentContract)
    {
        if (ModelState.IsValid)
        {
            await _employmentContractRepository.Create(employmentContract);

            return RedirectToAction("Index");
        }

        var mappedEmploymentContract = _mapper.Map<CreateViewModel>(employmentContract);

        var allContractTypes = await _contractTypeRepository.GetAll();
        mappedEmploymentContract.ContractTypes = allContractTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedEmploymentContract.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedEmploymentContract);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employmentContract = await _employmentContractRepository.GetById(id);
        if (employmentContract == null)
            return NotFound();

        var mappedEmploymentContract = _mapper.Map<UpdateViewModel>(employmentContract);

        var allContractTypes = await _contractTypeRepository.GetAll();
        mappedEmploymentContract.ContractTypes = allContractTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedEmploymentContract.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedEmploymentContract);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmploymentContractUpdateDto employmentContract)
    {
        if (ModelState.IsValid)
        {
            await _employmentContractRepository.Update(employmentContract);
            return RedirectToAction("Index");
        }

        var mappedEmploymentContract = _mapper.Map<UpdateViewModel>(employmentContract);

        var allContractTypes = await _contractTypeRepository.GetAll();
        mappedEmploymentContract.ContractTypes = allContractTypes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.TypeName });

        var allEmployees = await _employeeRepository.GetAll();
        mappedEmploymentContract.Employees = allEmployees.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.FullName });

        return View(mappedEmploymentContract);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employmentContract = await _employmentContractRepository.GetById(id);
        if (employmentContract == null)
            return NotFound();

        var mappedEmploymentContract = _mapper.Map<DeleteViewModel>(employmentContract);

        return View(mappedEmploymentContract);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employmentContractRepository.Delete(id);

        return RedirectToAction("Index");
    }
}