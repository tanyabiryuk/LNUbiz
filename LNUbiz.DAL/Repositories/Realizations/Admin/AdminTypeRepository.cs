using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories.Contracts;

namespace LNUbiz.DAL.Repositories
{
    public class AdminTypeRepository : RepositoryBase<AdminType>, IAdminTypeRepository
    {
        public AdminTypeRepository(LNUbizDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
