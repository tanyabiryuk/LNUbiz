using AutoMapper;
using LNUbiz.BLL.DTO.Admin;
using LNUbiz.DAL.Entities;
using LNUbiz.Web.Models.Admin;

namespace LNUbiz.Web.Mapping.Admin
{
    public class AdminTypeProfile : Profile
    {
        public AdminTypeProfile()
        {
            CreateMap<AdminType, AdminTypeDTO>().ReverseMap();
            CreateMap<AdminTypeDTO, AdminTypeViewModel>().ReverseMap();
        }
    }
}
