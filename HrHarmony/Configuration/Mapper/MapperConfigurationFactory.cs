using AutoMapper;
using HrHarmony.Data.Repositories.QueryBuilder.Pagination;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Models.Shared;
using HrHarmony.Models.ViewModels;

namespace HrHarmony.Configuration.Mapper;

public static class MapperConfigurationFactory
{
    public static MapperConfiguration Configure()
    {
        return  new MapperConfiguration(cfg =>
        {
            // Entity => Entity
            cfg.CreateMap<Absence, Absence>();
            cfg.CreateMap<Employee, Employee>();
            cfg.CreateMap<EmploymentContract, EmploymentContract>();
            cfg.CreateMap<Leave, Leave>();
            cfg.CreateMap<Salary, Salary>();

            cfg.CreateMap<AbsenceType, AbsenceType>();
            cfg.CreateMap<Address, Address>();
            cfg.CreateMap<ContractType, ContractType>();
            cfg.CreateMap<EducationLevel, EducationLevel>();
            cfg.CreateMap<Experience, Experience>();
            cfg.CreateMap<LeaveType, LeaveType>();
            cfg.CreateMap<MaritalStatus, MaritalStatus>();


            // Entity => Dto
            cfg.CreateMap<Absence, AbsenceDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<EmploymentContract, EmploymentContractDto>();
            cfg.CreateMap<Leave, LeaveDto>();
            cfg.CreateMap<Salary, SalaryDto>();

            cfg.CreateMap<AbsenceType, AbsenceTypeDto>();
            cfg.CreateMap<Address, AddressDto>();
            cfg.CreateMap<ContractType, ContractTypeDto>();
            cfg.CreateMap<EducationLevel, EducationLevelDto>();
            cfg.CreateMap<Experience, ExperienceDto>();
            cfg.CreateMap<LeaveType, LeaveTypeDto>();
            cfg.CreateMap<MaritalStatus, MaritalStatusDto>();


            // CreateDto => Entity
            cfg.CreateMap<AbsenceCreateDto, Absence>();
            cfg.CreateMap<EmployeeCreateDto, Employee>();
            cfg.CreateMap<EmploymentContractCreateDto, EmploymentContract>();
            cfg.CreateMap<LeaveCreateDto, Leave>();
            cfg.CreateMap<SalaryCreateDto, Salary>();

            cfg.CreateMap<AbsenceTypeCreateDto, AbsenceType>();
            cfg.CreateMap<AddressCreateDto, Address>();
            cfg.CreateMap<ContractTypeCreateDto, ContractType>();
            cfg.CreateMap<EducationLevelCreateDto, EducationLevel>();
            cfg.CreateMap<ExperienceCreateDto, Experience>();
            cfg.CreateMap<LeaveTypeCreateDto, LeaveType>();
            cfg.CreateMap<MaritalStatusCreateDto, MaritalStatus>();


            // UpdateDto => Entity
            cfg.CreateMap<AbsenceUpdateDto, Absence>();
            cfg.CreateMap<EmployeeUpdateDto, Employee>();
            cfg.CreateMap<EmploymentContractUpdateDto, EmploymentContract>();
            cfg.CreateMap<LeaveUpdateDto, Leave>();
            cfg.CreateMap<SalaryUpdateDto, Salary>();

            cfg.CreateMap<AbsenceTypeUpdateDto, AbsenceType>();
            cfg.CreateMap<AddressUpdateDto, Address>();
            cfg.CreateMap<ContractTypeUpdateDto, ContractType>();
            cfg.CreateMap<EducationLevelUpdateDto, EducationLevel>();
            cfg.CreateMap<ExperienceUpdateDto, Experience>();
            cfg.CreateMap<LeaveTypeUpdateDto, LeaveType>();
            cfg.CreateMap<MaritalStatusUpdateDto, MaritalStatus>();


            // ============== View models ===================


            // Absence view models
            cfg.CreateMap<AbsenceDto, Models.ViewModels.Absence.IndexViewModel>();
            cfg.CreateMap<AbsenceDto, Models.ViewModels.Absence.DetailsViewModel>();
            cfg.CreateMap<AbsenceCreateDto, Models.ViewModels.Absence.CreateViewModel>();
            cfg.CreateMap<AbsenceDto, Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<AbsenceUpdateDto, Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<AbsenceDto, Models.ViewModels.Absence.DeleteViewModel>();

            cfg.CreateMap<Absence, Models.ViewModels.Absence.IndexViewModel>();
            cfg.CreateMap<Absence, Models.ViewModels.Absence.DetailsViewModel>();
            cfg.CreateMap<Absence, Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<Absence, Models.ViewModels.Absence.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AbsenceDto>, PagedRecordsViewModel<Models.ViewModels.Absence.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Absence.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Absence.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Absence>, PaginatedResult<Models.ViewModels.Absence.IndexViewModel>>();


            // Employee view models
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.IndexViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.DetailsViewModel>();
            cfg.CreateMap<EmployeeCreateDto, Models.ViewModels.Employee.CreateViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeUpdateDto, Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.DeleteViewModel>();

            cfg.CreateMap<Employee, Models.ViewModels.Employee.IndexViewModel>();
            cfg.CreateMap<Employee, Models.ViewModels.Employee.DetailsViewModel>();
            cfg.CreateMap<Employee, Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<Employee, Models.ViewModels.Employee.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EmployeeDto>, PagedRecordsViewModel<Models.ViewModels.Employee.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Employee.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Employee.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Employee>, PaginatedResult<Models.ViewModels.Employee.IndexViewModel>>();

    
            // EmploymentContract view models
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.IndexViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.DetailsViewModel>();
            cfg.CreateMap<EmploymentContractCreateDto, Models.ViewModels.EmploymentContract.CreateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractUpdateDto, Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.DeleteViewModel>();

            cfg.CreateMap<EmploymentContract, Models.ViewModels.EmploymentContract.IndexViewModel>();
            cfg.CreateMap<EmploymentContract, Models.ViewModels.EmploymentContract.DetailsViewModel>();
            cfg.CreateMap<EmploymentContract, Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContract, Models.ViewModels.EmploymentContract.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EmploymentContractDto>, PagedRecordsViewModel<Models.ViewModels.EmploymentContract.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.EmploymentContract.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.EmploymentContract.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<EmploymentContract>, PaginatedResult<Models.ViewModels.EmploymentContract.IndexViewModel>>();


            // Leave view models
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.IndexViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.DetailsViewModel>();
            cfg.CreateMap<LeaveCreateDto, Models.ViewModels.Leave.CreateViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveUpdateDto, Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.DeleteViewModel>();

            cfg.CreateMap<Leave, Models.ViewModels.Leave.IndexViewModel>();
            cfg.CreateMap<Leave, Models.ViewModels.Leave.DetailsViewModel>();
            cfg.CreateMap<Leave, Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<Leave, Models.ViewModels.Leave.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<LeaveDto>, PagedRecordsViewModel<Models.ViewModels.Leave.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Leave.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Leave.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Leave>, PaginatedResult<Models.ViewModels.Leave.IndexViewModel>>();


            // Salary view models
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.IndexViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.DetailsViewModel>();
            cfg.CreateMap<SalaryCreateDto, Models.ViewModels.Salary.CreateViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryUpdateDto, Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.DeleteViewModel>();

            cfg.CreateMap<Salary, Models.ViewModels.Salary.IndexViewModel>();
            cfg.CreateMap<Salary, Models.ViewModels.Salary.DetailsViewModel>();
            cfg.CreateMap<Salary, Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<Salary, Models.ViewModels.Salary.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<SalaryDto>, PagedRecordsViewModel<Models.ViewModels.Salary.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Salary.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Salary.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Salary>, PaginatedResult<Models.ViewModels.Salary.IndexViewModel>>();




            // AbsenceType view models
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.IndexViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.DetailsViewModel>();
            cfg.CreateMap<AbsenceTypeCreateDto, Models.ViewModels.AbsenceType.CreateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeUpdateDto, Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.DeleteViewModel>();

            cfg.CreateMap<AbsenceType, Models.ViewModels.AbsenceType.IndexViewModel>();
            cfg.CreateMap<AbsenceType, Models.ViewModels.AbsenceType.DetailsViewModel>();
            cfg.CreateMap<AbsenceType, Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceType, Models.ViewModels.AbsenceType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AbsenceTypeDto>, PagedRecordsViewModel<Models.ViewModels.AbsenceType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.AbsenceType.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.AbsenceType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<AbsenceType>, PaginatedResult<Models.ViewModels.AbsenceType.IndexViewModel>>();


            // Address view models
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.IndexViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.DetailsViewModel>();
            cfg.CreateMap<AddressCreateDto, Models.ViewModels.Address.CreateViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressUpdateDto, Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.DeleteViewModel>();

            cfg.CreateMap<Address, Models.ViewModels.Address.IndexViewModel>();
            cfg.CreateMap<Address, Models.ViewModels.Address.DetailsViewModel>();
            cfg.CreateMap<Address, Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<Address, Models.ViewModels.Address.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AddressDto>, PagedRecordsViewModel<Models.ViewModels.Address.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Address.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Address.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Address>, PaginatedResult<Models.ViewModels.Address.IndexViewModel>>();


            // ContractType view models
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.IndexViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.DetailsViewModel>();
            cfg.CreateMap<ContractTypeCreateDto, Models.ViewModels.ContractType.CreateViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeUpdateDto, Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.DeleteViewModel>();

            cfg.CreateMap<ContractType, Models.ViewModels.ContractType.IndexViewModel>();
            cfg.CreateMap<ContractType, Models.ViewModels.ContractType.DetailsViewModel>();
            cfg.CreateMap<ContractType, Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractType, Models.ViewModels.ContractType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<ContractTypeDto>, PagedRecordsViewModel<Models.ViewModels.ContractType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.ContractType.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.ContractType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<ContractType>, PaginatedResult<Models.ViewModels.ContractType.IndexViewModel>>();


            // EducationLevel view models
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.IndexViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.DetailsViewModel>();
            cfg.CreateMap<EducationLevelCreateDto, Models.ViewModels.EducationLevel.CreateViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelUpdateDto, Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.DeleteViewModel>();

            cfg.CreateMap<EducationLevel, Models.ViewModels.EducationLevel.IndexViewModel>();
            cfg.CreateMap<EducationLevel, Models.ViewModels.EducationLevel.DetailsViewModel>();
            cfg.CreateMap<EducationLevel, Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevel, Models.ViewModels.EducationLevel.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EducationLevelDto>, PagedRecordsViewModel<Models.ViewModels.EducationLevel.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.EducationLevel.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.EducationLevel.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<EducationLevel>, PaginatedResult<Models.ViewModels.EducationLevel.IndexViewModel>>();


            // Experience view models
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.IndexViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.DetailsViewModel>();
            cfg.CreateMap<ExperienceCreateDto, Models.ViewModels.Experience.CreateViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceUpdateDto, Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.DeleteViewModel>();

            cfg.CreateMap<Experience, Models.ViewModels.Experience.IndexViewModel>();
            cfg.CreateMap<Experience, Models.ViewModels.Experience.DetailsViewModel>();
            cfg.CreateMap<Experience, Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<Experience, Models.ViewModels.Experience.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<ExperienceDto>, PagedRecordsViewModel<Models.ViewModels.Experience.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.Experience.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.Experience.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Experience>, PaginatedResult<Models.ViewModels.Experience.IndexViewModel>>();


            // LeaveType view models
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.IndexViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.DetailsViewModel>();
            cfg.CreateMap<LeaveTypeCreateDto, Models.ViewModels.LeaveType.CreateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeUpdateDto, Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.DeleteViewModel>();

            cfg.CreateMap<LeaveType, Models.ViewModels.LeaveType.IndexViewModel>();
            cfg.CreateMap<LeaveType, Models.ViewModels.LeaveType.DetailsViewModel>();
            cfg.CreateMap<LeaveType, Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveType, Models.ViewModels.LeaveType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<LeaveTypeDto>, PagedRecordsViewModel<Models.ViewModels.LeaveType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.LeaveType.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.LeaveType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<LeaveType>, PaginatedResult<Models.ViewModels.LeaveType.IndexViewModel>>();


            // MaritalStatus view models
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.IndexViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.DetailsViewModel>();
            cfg.CreateMap<MaritalStatusCreateDto, Models.ViewModels.MaritalStatus.CreateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusUpdateDto, Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.DeleteViewModel>();

            cfg.CreateMap<MaritalStatus, Models.ViewModels.MaritalStatus.IndexViewModel>();
            cfg.CreateMap<MaritalStatus, Models.ViewModels.MaritalStatus.DetailsViewModel>();
            cfg.CreateMap<MaritalStatus, Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatus, Models.ViewModels.MaritalStatus.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<MaritalStatusDto>, PagedRecordsViewModel<Models.ViewModels.MaritalStatus.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Models.ViewModels.MaritalStatus.IndexViewModel>, PagedRecordsViewModel<Models.ViewModels.MaritalStatus.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<MaritalStatus>, PaginatedResult<Models.ViewModels.MaritalStatus.IndexViewModel>>();
        });
    }
}