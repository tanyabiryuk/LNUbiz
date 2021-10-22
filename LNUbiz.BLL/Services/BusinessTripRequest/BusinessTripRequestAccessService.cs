using AutoMapper;
using LNUbiz.BLL.Settings;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.BusinessTripRequest;
using LNUbiz.DAL.Repositories;
using LNUbiz.BLL.Interfaces;
using DatabaseEntities = LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Services
{
    public class BusinessTripRequestAccessService : IBusinessTripRequestAccessService
    {
        private readonly UserManager<DatabaseEntities.User> _userManager;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, IBusinessTripRequestAccessGetter> _businessTripRequestAccessGettersAccessGetters;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BusinessTripRequestAccessService(BusinessTripRequestAccessSettings settings, UserManager<DatabaseEntities.User> userManager,
            IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _businessTripRequestAccessGettersAccessGetters = settings.BusinessTripRequestAccessGetters;
            _userManager = userManager;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<BusinessTripRequestDTO>> GetRequestsAsync(DatabaseEntities.User claimsPrincipal)
        {
            var roles = await _userManager.GetRolesAsync(claimsPrincipal);
            foreach (var key in _businessTripRequestAccessGettersAccessGetters.Keys)
            {
                if (roles.Contains(key))
                {
                    var reuests = await _businessTripRequestAccessGettersAccessGetters[key].GetRequestAsync(claimsPrincipal.Id);
                    return _mapper.Map<IEnumerable<DatabaseEntities.BusinessTripRequest>, IEnumerable<BusinessTripRequestDTO>>(reuests);
                }
            }
            return Enumerable.Empty<BusinessTripRequestDTO>();
        }

        public async Task<bool> HasAccessAsync(DatabaseEntities.User claimsPrincipal, int requestId)
        {
            var reuests = await GetRequestsAsync(claimsPrincipal);
            return reuests.Any(c => c.Id == requestId);
        }
    }
}
