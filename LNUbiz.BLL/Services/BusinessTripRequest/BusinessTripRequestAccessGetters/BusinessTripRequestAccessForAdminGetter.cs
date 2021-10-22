using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LNUbiz.Resources;

namespace LNUbiz.BLL.Services
{
    public class BusinessTripRequestAccessForAdminGetter : IBusinessTripRequestAccessGetter
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly AdminType _AdminType;

        public BusinessTripRequestAccessForAdminGetter(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _AdminType = _repositoryWrapper.AdminType.GetFirstAsync(
                predicate: a => a.AdminTypeName == Roles.Admin).Result;
        }

        public async Task<IEnumerable<BusinessTripRequest>> GetRequestAsync(string userId)
        {
            return await _repositoryWrapper.BusinessTripRequests.GetAllAsync(predicate: r => r.UserId == userId);

        }
    }
}
