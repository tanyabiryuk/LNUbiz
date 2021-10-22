using LNUbiz.DAL.Entities;

namespace LNUbiz.DAL.Repositories
{
    public class ConfirmedUserRepository : RepositoryBase<ConfirmedUser>, IConfirmedUserRepository
    {
        public ConfirmedUserRepository(LNUbizDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
