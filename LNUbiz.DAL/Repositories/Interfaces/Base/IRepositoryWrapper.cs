using LNUbiz.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace LNUbiz.DAL.Repositories
{
    public interface IRepositoryWrapper
    {
        IAdminTypeRepository AdminType { get; }
        IBusinessTripRequestRepository BusinessTripRequests { get; }
        IConfirmedUserRepository ConfirmedUser { get; }
        INotificationTypeRepository NotificationTypes { get; }
        IUserRepository User { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Save();

        Task SaveAsync();
    }
}
