using App.DAL.Models;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.ViewModels;
using AutoMapper;

namespace AppRouteSession03.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<DepartmentViewModel , Department>().ReverseMap();
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
        }
    }
}
