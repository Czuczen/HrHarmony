using AutoMapper;
using HrHarmony.Data.Models.Dto.Create.Dictionary;
using HrHarmony.Data.Models.Dto.Create.Main;
using HrHarmony.Data.Models.Dto.Details.Dictionary;
using HrHarmony.Data.Models.Dto.Details.Main;
using HrHarmony.Data.Models.Dto.Update.Dictionary;
using HrHarmony.Data.Models.Dto.Update.Main;
using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.ViewModels.Absence;
using HrHarmony.Data.Models.ViewModels.AbsenceType;
using HrHarmony.Data.Models.ViewModels.Address;
using HrHarmony.Data.Models.ViewModels.ContractType;
using HrHarmony.Data.Models.ViewModels.EducationLevel;
using HrHarmony.Data.Models.ViewModels.Employee;
using HrHarmony.Data.Models.ViewModels.EmploymentContract;
using HrHarmony.Data.Models.ViewModels.Experience;
using HrHarmony.Data.Models.ViewModels.Leave;
using HrHarmony.Data.Models.ViewModels.LeaveType;
using HrHarmony.Data.Models.ViewModels.MaritalStatus;
using HrHarmony.Data.Models.ViewModels.Salary;
using HrHarmony.Data.Repositories.QueryBuilder.Pagination;

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
            cfg.CreateMap<AbsenceDto, Data.Models.ViewModels.Absence.IndexViewModel>();
            cfg.CreateMap<AbsenceDto, Data.Models.ViewModels.Absence.DetailsViewModel>();
            cfg.CreateMap<AbsenceCreateDto, Data.Models.ViewModels.Absence.CreateViewModel>();
            cfg.CreateMap<AbsenceDto, Data.Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<AbsenceUpdateDto, Data.Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<AbsenceDto, Data.Models.ViewModels.Absence.DeleteViewModel>();

            cfg.CreateMap<Absence, Data.Models.ViewModels.Absence.IndexViewModel>();
            cfg.CreateMap<Absence, Data.Models.ViewModels.Absence.DetailsViewModel>();
            cfg.CreateMap<Absence, Data.Models.ViewModels.Absence.UpdateViewModel>();
            cfg.CreateMap<Absence, Data.Models.ViewModels.Absence.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AbsenceDto>, PagedRecordsViewModel<Data.Models.ViewModels.Absence.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Absence.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Absence.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Absence>, PaginatedResult<Data.Models.ViewModels.Absence.IndexViewModel>>();


            // Employee view models
            cfg.CreateMap<EmployeeDto, Data.Models.ViewModels.Employee.IndexViewModel>();
            cfg.CreateMap<EmployeeDto, Data.Models.ViewModels.Employee.DetailsViewModel>();
            cfg.CreateMap<EmployeeCreateDto, Data.Models.ViewModels.Employee.CreateViewModel>();
            cfg.CreateMap<EmployeeDto, Data.Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeUpdateDto, Data.Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeDto, Data.Models.ViewModels.Employee.DeleteViewModel>();

            cfg.CreateMap<Employee, Data.Models.ViewModels.Employee.IndexViewModel>();
            cfg.CreateMap<Employee, Data.Models.ViewModels.Employee.DetailsViewModel>();
            cfg.CreateMap<Employee, Data.Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<Employee, Data.Models.ViewModels.Employee.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EmployeeDto>, PagedRecordsViewModel<Data.Models.ViewModels.Employee.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Employee.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Employee.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Employee>, PaginatedResult<Data.Models.ViewModels.Employee.IndexViewModel>>();

    
            // EmploymentContract view models
            cfg.CreateMap<EmploymentContractDto, Data.Models.ViewModels.EmploymentContract.IndexViewModel>();
            cfg.CreateMap<EmploymentContractDto, Data.Models.ViewModels.EmploymentContract.DetailsViewModel>();
            cfg.CreateMap<EmploymentContractCreateDto, Data.Models.ViewModels.EmploymentContract.CreateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Data.Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractUpdateDto, Data.Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Data.Models.ViewModels.EmploymentContract.DeleteViewModel>();

            cfg.CreateMap<EmploymentContract, Data.Models.ViewModels.EmploymentContract.IndexViewModel>();
            cfg.CreateMap<EmploymentContract, Data.Models.ViewModels.EmploymentContract.DetailsViewModel>();
            cfg.CreateMap<EmploymentContract, Data.Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContract, Data.Models.ViewModels.EmploymentContract.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EmploymentContractDto>, PagedRecordsViewModel<Data.Models.ViewModels.EmploymentContract.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.EmploymentContract.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.EmploymentContract.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<EmploymentContract>, PaginatedResult<Data.Models.ViewModels.EmploymentContract.IndexViewModel>>();


            // Leave view models
            cfg.CreateMap<LeaveDto, Data.Models.ViewModels.Leave.IndexViewModel>();
            cfg.CreateMap<LeaveDto, Data.Models.ViewModels.Leave.DetailsViewModel>();
            cfg.CreateMap<LeaveCreateDto, Data.Models.ViewModels.Leave.CreateViewModel>();
            cfg.CreateMap<LeaveDto, Data.Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveUpdateDto, Data.Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveDto, Data.Models.ViewModels.Leave.DeleteViewModel>();

            cfg.CreateMap<Leave, Data.Models.ViewModels.Leave.IndexViewModel>();
            cfg.CreateMap<Leave, Data.Models.ViewModels.Leave.DetailsViewModel>();
            cfg.CreateMap<Leave, Data.Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<Leave, Data.Models.ViewModels.Leave.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<LeaveDto>, PagedRecordsViewModel<Data.Models.ViewModels.Leave.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Leave.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Leave.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Leave>, PaginatedResult<Data.Models.ViewModels.Leave.IndexViewModel>>();


            // Salary view models
            cfg.CreateMap<SalaryDto, Data.Models.ViewModels.Salary.IndexViewModel>();
            cfg.CreateMap<SalaryDto, Data.Models.ViewModels.Salary.DetailsViewModel>();
            cfg.CreateMap<SalaryCreateDto, Data.Models.ViewModels.Salary.CreateViewModel>();
            cfg.CreateMap<SalaryDto, Data.Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryUpdateDto, Data.Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryDto, Data.Models.ViewModels.Salary.DeleteViewModel>();

            cfg.CreateMap<Salary, Data.Models.ViewModels.Salary.IndexViewModel>();
            cfg.CreateMap<Salary, Data.Models.ViewModels.Salary.DetailsViewModel>();
            cfg.CreateMap<Salary, Data.Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<Salary, Data.Models.ViewModels.Salary.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<SalaryDto>, PagedRecordsViewModel<Data.Models.ViewModels.Salary.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Salary.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Salary.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Salary>, PaginatedResult<Data.Models.ViewModels.Salary.IndexViewModel>>();




            // AbsenceType view models
            cfg.CreateMap<AbsenceTypeDto, Data.Models.ViewModels.AbsenceType.IndexViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Data.Models.ViewModels.AbsenceType.DetailsViewModel>();
            cfg.CreateMap<AbsenceTypeCreateDto, Data.Models.ViewModels.AbsenceType.CreateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Data.Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeUpdateDto, Data.Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Data.Models.ViewModels.AbsenceType.DeleteViewModel>();

            cfg.CreateMap<AbsenceType, Data.Models.ViewModels.AbsenceType.IndexViewModel>();
            cfg.CreateMap<AbsenceType, Data.Models.ViewModels.AbsenceType.DetailsViewModel>();
            cfg.CreateMap<AbsenceType, Data.Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceType, Data.Models.ViewModels.AbsenceType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AbsenceTypeDto>, PagedRecordsViewModel<Data.Models.ViewModels.AbsenceType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.AbsenceType.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.AbsenceType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<AbsenceType>, PaginatedResult<Data.Models.ViewModels.AbsenceType.IndexViewModel>>();


            // Address view models
            cfg.CreateMap<AddressDto, Data.Models.ViewModels.Address.IndexViewModel>();
            cfg.CreateMap<AddressDto, Data.Models.ViewModels.Address.DetailsViewModel>();
            cfg.CreateMap<AddressCreateDto, Data.Models.ViewModels.Address.CreateViewModel>();
            cfg.CreateMap<AddressDto, Data.Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressUpdateDto, Data.Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressDto, Data.Models.ViewModels.Address.DeleteViewModel>();

            cfg.CreateMap<Address, Data.Models.ViewModels.Address.IndexViewModel>();
            cfg.CreateMap<Address, Data.Models.ViewModels.Address.DetailsViewModel>();
            cfg.CreateMap<Address, Data.Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<Address, Data.Models.ViewModels.Address.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<AddressDto>, PagedRecordsViewModel<Data.Models.ViewModels.Address.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Address.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Address.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Address>, PaginatedResult<Data.Models.ViewModels.Address.IndexViewModel>>();


            // ContractType view models
            cfg.CreateMap<ContractTypeDto, Data.Models.ViewModels.ContractType.IndexViewModel>();
            cfg.CreateMap<ContractTypeDto, Data.Models.ViewModels.ContractType.DetailsViewModel>();
            cfg.CreateMap<ContractTypeCreateDto, Data.Models.ViewModels.ContractType.CreateViewModel>();
            cfg.CreateMap<ContractTypeDto, Data.Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeUpdateDto, Data.Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeDto, Data.Models.ViewModels.ContractType.DeleteViewModel>();

            cfg.CreateMap<ContractType, Data.Models.ViewModels.ContractType.IndexViewModel>();
            cfg.CreateMap<ContractType, Data.Models.ViewModels.ContractType.DetailsViewModel>();
            cfg.CreateMap<ContractType, Data.Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractType, Data.Models.ViewModels.ContractType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<ContractTypeDto>, PagedRecordsViewModel<Data.Models.ViewModels.ContractType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.ContractType.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.ContractType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<ContractType>, PaginatedResult<Data.Models.ViewModels.ContractType.IndexViewModel>>();


            // EducationLevel view models
            cfg.CreateMap<EducationLevelDto, Data.Models.ViewModels.EducationLevel.IndexViewModel>();
            cfg.CreateMap<EducationLevelDto, Data.Models.ViewModels.EducationLevel.DetailsViewModel>();
            cfg.CreateMap<EducationLevelCreateDto, Data.Models.ViewModels.EducationLevel.CreateViewModel>();
            cfg.CreateMap<EducationLevelDto, Data.Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelUpdateDto, Data.Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelDto, Data.Models.ViewModels.EducationLevel.DeleteViewModel>();

            cfg.CreateMap<EducationLevel, Data.Models.ViewModels.EducationLevel.IndexViewModel>();
            cfg.CreateMap<EducationLevel, Data.Models.ViewModels.EducationLevel.DetailsViewModel>();
            cfg.CreateMap<EducationLevel, Data.Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevel, Data.Models.ViewModels.EducationLevel.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<EducationLevelDto>, PagedRecordsViewModel<Data.Models.ViewModels.EducationLevel.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.EducationLevel.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.EducationLevel.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<EducationLevel>, PaginatedResult<Data.Models.ViewModels.EducationLevel.IndexViewModel>>();


            // Experience view models
            cfg.CreateMap<ExperienceDto, Data.Models.ViewModels.Experience.IndexViewModel>();
            cfg.CreateMap<ExperienceDto, Data.Models.ViewModels.Experience.DetailsViewModel>();
            cfg.CreateMap<ExperienceCreateDto, Data.Models.ViewModels.Experience.CreateViewModel>();
            cfg.CreateMap<ExperienceDto, Data.Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceUpdateDto, Data.Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceDto, Data.Models.ViewModels.Experience.DeleteViewModel>();

            cfg.CreateMap<Experience, Data.Models.ViewModels.Experience.IndexViewModel>();
            cfg.CreateMap<Experience, Data.Models.ViewModels.Experience.DetailsViewModel>();
            cfg.CreateMap<Experience, Data.Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<Experience, Data.Models.ViewModels.Experience.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<ExperienceDto>, PagedRecordsViewModel<Data.Models.ViewModels.Experience.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.Experience.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.Experience.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<Experience>, PaginatedResult<Data.Models.ViewModels.Experience.IndexViewModel>>();


            // LeaveType view models
            cfg.CreateMap<LeaveTypeDto, Data.Models.ViewModels.LeaveType.IndexViewModel>();
            cfg.CreateMap<LeaveTypeDto, Data.Models.ViewModels.LeaveType.DetailsViewModel>();
            cfg.CreateMap<LeaveTypeCreateDto, Data.Models.ViewModels.LeaveType.CreateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Data.Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeUpdateDto, Data.Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Data.Models.ViewModels.LeaveType.DeleteViewModel>();

            cfg.CreateMap<LeaveType, Data.Models.ViewModels.LeaveType.IndexViewModel>();
            cfg.CreateMap<LeaveType, Data.Models.ViewModels.LeaveType.DetailsViewModel>();
            cfg.CreateMap<LeaveType, Data.Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveType, Data.Models.ViewModels.LeaveType.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<LeaveTypeDto>, PagedRecordsViewModel<Data.Models.ViewModels.LeaveType.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.LeaveType.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.LeaveType.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<LeaveType>, PaginatedResult<Data.Models.ViewModels.LeaveType.IndexViewModel>>();


            // MaritalStatus view models
            cfg.CreateMap<MaritalStatusDto, Data.Models.ViewModels.MaritalStatus.IndexViewModel>();
            cfg.CreateMap<MaritalStatusDto, Data.Models.ViewModels.MaritalStatus.DetailsViewModel>();
            cfg.CreateMap<MaritalStatusCreateDto, Data.Models.ViewModels.MaritalStatus.CreateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Data.Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusUpdateDto, Data.Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Data.Models.ViewModels.MaritalStatus.DeleteViewModel>();

            cfg.CreateMap<MaritalStatus, Data.Models.ViewModels.MaritalStatus.IndexViewModel>();
            cfg.CreateMap<MaritalStatus, Data.Models.ViewModels.MaritalStatus.DetailsViewModel>();
            cfg.CreateMap<MaritalStatus, Data.Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatus, Data.Models.ViewModels.MaritalStatus.DeleteViewModel>();

            cfg.CreateMap<PaginatedResult<MaritalStatusDto>, PagedRecordsViewModel<Data.Models.ViewModels.MaritalStatus.IndexViewModel>>();
            cfg.CreateMap<PaginatedResult<Data.Models.ViewModels.MaritalStatus.IndexViewModel>, PagedRecordsViewModel<Data.Models.ViewModels.MaritalStatus.IndexViewModel>>();
            cfg.CreateMap<PaginatedQuery<MaritalStatus>, PaginatedResult<Data.Models.ViewModels.MaritalStatus.IndexViewModel>>();
        });
    }
}