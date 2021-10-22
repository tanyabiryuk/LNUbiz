using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseEntities = LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Services
{
    public interface IBusinessTripRequestAccessGetter
    {
        Task<IEnumerable<DatabaseEntities.BusinessTripRequest>> GetRequestAsync(string userId);
    }
}
