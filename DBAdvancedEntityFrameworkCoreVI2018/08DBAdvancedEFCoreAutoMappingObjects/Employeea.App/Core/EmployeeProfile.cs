using AutoMapper;
using Employees.App.Core.DTOs;
using Employees.Models;

namespace Employees.App.Core
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>()
                .ForMember(destination => destination.EmployeeDtos, from => from.MapFrom(e => e.ManagerEmployees))
                .ReverseMap();
            CreateMap<Employee, EmployeeOfficeInfoDto>().ReverseMap();
            //CreateMap<Employee, EmployeeOfficeInfoDto>()
            //    .ForMember(destination => destination.Manager, from => from.MapFrom(e => e.Manager))
            //    .ReverseMap();
        }
    }
}