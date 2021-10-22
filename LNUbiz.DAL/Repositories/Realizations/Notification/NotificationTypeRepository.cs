using LNUbiz.DAL.Entities;

namespace LNUbiz.DAL.Repositories
{
    public class NotificationTypeRepository : RepositoryBase<NotificationType>, INotificationTypeRepository
    {
        public NotificationTypeRepository(LNUbizDBContext dbContext) : base(dbContext)
        {
        }
    }
}
