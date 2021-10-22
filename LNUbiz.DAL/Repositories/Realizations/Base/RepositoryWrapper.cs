using LNUbiz.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace LNUbiz.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LNUbizDBContext _dbContext;
        private IUserRepository _user;
        
        private IConfirmedUserRepository _confirmedUser;

        private IAdminTypeRepository _admintype;

        private IBusinessTripRequestRepository _businessTripRequests;
        private IUserNotificationRepository _userNotifications;
        private INotificationTypeRepository _notificationTypes;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_dbContext);
                }

                return _user;
            }
        }


        public IConfirmedUserRepository ConfirmedUser
        {
            get
            {
                if (_confirmedUser == null)
                {
                    _confirmedUser = new ConfirmedUserRepository(_dbContext);
                }

                return _confirmedUser;
            }
        }

        public IAdminTypeRepository AdminType
        {
            get
            {
                if (_admintype == null)
                {
                    _admintype = new AdminTypeRepository(_dbContext);
                }

                return _admintype;
            }
        }

        public IBusinessTripRequestRepository BusinessTripRequests
        {
            get
            {
                if (_businessTripRequests == null)
                {
                    _businessTripRequests = new BusinessTripRequestRepository(_dbContext);
                }
                return _businessTripRequests;
            }
        }

        public IUserNotificationRepository UserNotifications
        {
            get
            {
                if (_userNotifications == null)
                {
                    _userNotifications = new UserNotificationRepository(_dbContext);
                }
                return _userNotifications;
            }
        }

        public INotificationTypeRepository NotificationTypes
        {
            get
            {
                if (_notificationTypes == null)
                {
                    _notificationTypes = new NotificationTypeRepository(_dbContext);
                }
                return _notificationTypes;
            }
        }

        public RepositoryWrapper(LNUbizDBContext _LNUbizDBContext)
        {
            _dbContext = _LNUbizDBContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}