using LNUbiz.DAL.Entities;

namespace LNUbiz.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LNUbizDBContext dbContext)
            : base(dbContext)
        {
        }

        public new void Update(User item)
        {
            var user = LNUbizDBContext.Users.Find(item.Id);
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.FatherName = item.FatherName;
            user.ImagePath = item.ImagePath;
            user.PhoneNumber = item.PhoneNumber;
            LNUbizDBContext.Users.Update(user);
        }
    }
}
