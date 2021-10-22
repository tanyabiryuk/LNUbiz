using System.Collections.Generic;
using LNUbiz.BLL.Services;
using LNUbiz.DAL.Repositories;
using LNUbiz.Resources;

namespace LNUbiz.BLL.Settings
{
    public class BusinessTripRequestAccessSettings
    {
        private const string AdminRoleName = Roles.Admin;
        private const string UserRoleName = Roles.User;

        private readonly IRepositoryWrapper _repositoryWrapper;

        public BusinessTripRequestAccessSettings(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Dictionary<string, IBusinessTripRequestAccessGetter> BusinessTripRequestAccessGetters
        {
            get
            {
                return new Dictionary<string, IBusinessTripRequestAccessGetter>
                {
                    { AdminRoleName,  new BusinessTripRequestAccessForAdminGetter(_repositoryWrapper) },
                    { UserRoleName, new BusinessTripRequestAccessForUserGetter(_repositoryWrapper) },
                };
            }
        }

    }
}
