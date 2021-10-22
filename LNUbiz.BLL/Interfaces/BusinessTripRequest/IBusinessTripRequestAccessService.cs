using LNUbiz.BLL.DTO.BusinessTripRequest;
using System.Collections.Generic;
using System.Threading.Tasks;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Interfaces
{
    public interface IBusinessTripRequestAccessService
    {
        Task<IEnumerable<BusinessTripRequestDTO>> GetRequestsAsync(User claimsPrincipal);
        Task<bool> HasAccessAsync(User claimsPrincipal, int requestId);
    }
}
