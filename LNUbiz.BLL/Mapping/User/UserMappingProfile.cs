using AutoMapper;
using LNUbiz.BLL.DTO.UserProfiles;

namespace LNUbiz.BLL.Mapping.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<DAL.Entities.User, UserDTO>().ReverseMap();
        }
    }
}
