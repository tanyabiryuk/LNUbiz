using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LNUbiz.BLL.Services
{
    public class BusinessTripRequestAccessForUserGetter : IBusinessTripRequestAccessGetter
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public BusinessTripRequestAccessForUserGetter(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<BusinessTripRequest>> GetRequestAsync(string userId)
        {
            return await _repositoryWrapper.BusinessTripRequests.GetAllAsync(predicate: r => r.UserId == userId);
        }

        public async Task<bool> CheckAccessAsync(string userId, int requestId)
        {
            return (await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                     predicate: r => r.Id == requestId 
                                  && r.UserId == userId)) != null;
        }
    }
}
