using AutoMapper;
using HrHarmony.Models.Dto.Create.Dictionary;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Dictionary;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Configuration.Mapper;

public static class MapperConfigurationFactory
{
    public static MapperConfiguration Configure()
    {
        return  new MapperConfiguration(cfg =>
        {
            // jeśli pole będzie null wartość będzie z obiektu, który posiada wartość
            cfg.CreateMap<Absence, Absence>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<Employee, Employee>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<EmploymentContract, EmploymentContract>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<Leave, Leave>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<Salary, Salary>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<AbsenceType, AbsenceType>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<Address, Address>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<ContractType, ContractType>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<EducationLevel, EducationLevel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<Experience, Experience>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<LeaveType, LeaveType>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            cfg.CreateMap<MaritalStatus, MaritalStatus>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            

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

            // Employee view models
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.IndexViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.DetailsViewModel>();
            cfg.CreateMap<EmployeeCreateDto, Models.ViewModels.Employee.CreateViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeUpdateDto, Models.ViewModels.Employee.UpdateViewModel>();
            cfg.CreateMap<EmployeeDto, Models.ViewModels.Employee.DeleteViewModel>();

            // EmploymentContract view models
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.IndexViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.DetailsViewModel>();
            cfg.CreateMap<EmploymentContractCreateDto, Models.ViewModels.EmploymentContract.CreateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractUpdateDto, Models.ViewModels.EmploymentContract.UpdateViewModel>();
            cfg.CreateMap<EmploymentContractDto, Models.ViewModels.EmploymentContract.DeleteViewModel>();

            // Leave view models
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.IndexViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.DetailsViewModel>();
            cfg.CreateMap<LeaveCreateDto, Models.ViewModels.Leave.CreateViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveUpdateDto, Models.ViewModels.Leave.UpdateViewModel>();
            cfg.CreateMap<LeaveDto, Models.ViewModels.Leave.DeleteViewModel>();

            // Salary view models
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.IndexViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.DetailsViewModel>();
            cfg.CreateMap<SalaryCreateDto, Models.ViewModels.Salary.CreateViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryUpdateDto, Models.ViewModels.Salary.UpdateViewModel>();
            cfg.CreateMap<SalaryDto, Models.ViewModels.Salary.DeleteViewModel>();




            // AbsenceType view models
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.IndexViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.DetailsViewModel>();
            cfg.CreateMap<AbsenceTypeCreateDto, Models.ViewModels.AbsenceType.CreateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeUpdateDto, Models.ViewModels.AbsenceType.UpdateViewModel>();
            cfg.CreateMap<AbsenceTypeDto, Models.ViewModels.AbsenceType.DeleteViewModel>();

            // Address view models
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.IndexViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.DetailsViewModel>();
            cfg.CreateMap<AddressCreateDto, Models.ViewModels.Address.CreateViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressUpdateDto, Models.ViewModels.Address.UpdateViewModel>();
            cfg.CreateMap<AddressDto, Models.ViewModels.Address.DeleteViewModel>();

            // ContractType view models
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.IndexViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.DetailsViewModel>();
            cfg.CreateMap<ContractTypeCreateDto, Models.ViewModels.ContractType.CreateViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeUpdateDto, Models.ViewModels.ContractType.UpdateViewModel>();
            cfg.CreateMap<ContractTypeDto, Models.ViewModels.ContractType.DeleteViewModel>();

            // EducationLevel view models
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.IndexViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.DetailsViewModel>();
            cfg.CreateMap<EducationLevelCreateDto, Models.ViewModels.EducationLevel.CreateViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelUpdateDto, Models.ViewModels.EducationLevel.UpdateViewModel>();
            cfg.CreateMap<EducationLevelDto, Models.ViewModels.EducationLevel.DeleteViewModel>();

            // Experience view models
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.IndexViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.DetailsViewModel>();
            cfg.CreateMap<ExperienceCreateDto, Models.ViewModels.Experience.CreateViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceUpdateDto, Models.ViewModels.Experience.UpdateViewModel>();
            cfg.CreateMap<ExperienceDto, Models.ViewModels.Experience.DeleteViewModel>();

            // LeaveType view models
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.IndexViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.DetailsViewModel>();
            cfg.CreateMap<LeaveTypeCreateDto, Models.ViewModels.LeaveType.CreateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeUpdateDto, Models.ViewModels.LeaveType.UpdateViewModel>();
            cfg.CreateMap<LeaveTypeDto, Models.ViewModels.LeaveType.DeleteViewModel>();

            // MaritalStatus view models
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.IndexViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.DetailsViewModel>();
            cfg.CreateMap<MaritalStatusCreateDto, Models.ViewModels.MaritalStatus.CreateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusUpdateDto, Models.ViewModels.MaritalStatus.UpdateViewModel>();
            cfg.CreateMap<MaritalStatusDto, Models.ViewModels.MaritalStatus.DeleteViewModel>();
            
        });
    }
}