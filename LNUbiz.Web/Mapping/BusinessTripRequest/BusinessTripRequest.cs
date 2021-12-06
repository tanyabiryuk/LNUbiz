using AutoMapper;
using LNUbiz.BLL.DTO.BusinessTripRequest;
using DatabaseEntities = LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Mapping.BusinessTripRequest
{
    public class BusinessTripRequest : Profile
    {
        public BusinessTripRequest()
        {
            CreateMap<DatabaseEntities.BusinessTripRequest, BusinessTripRequestDTO>().ReverseMap();
        }
    }
}