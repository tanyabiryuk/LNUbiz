using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories.Contracts;

namespace LNUbiz.DAL.Repositories
{
    public class BusinessTripRequestRepository : RepositoryBase<BusinessTripRequest>, IBusinessTripRequestRepository
    {
        public BusinessTripRequestRepository(LNUbizDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
