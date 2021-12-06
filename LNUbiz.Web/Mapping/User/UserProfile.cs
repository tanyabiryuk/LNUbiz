using AutoMapper;
using LNUbiz.BLL.DTO.UserProfiles;
using LNUbiz.Web.Models.UserModels;
namespace LNUbiz.Web.Mapping.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
        }
    }
}