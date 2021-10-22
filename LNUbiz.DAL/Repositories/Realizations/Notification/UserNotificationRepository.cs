using LNUbiz.DAL.Entities;

namespace LNUbiz.DAL.Repositories
{
    public class UserNotificationRepository : RepositoryBase<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(LNUbizDBContext dbContext)
             : base(dbContext)
        {
        }
    }
}
