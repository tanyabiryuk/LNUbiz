using LNUbiz.BLL.Interfaces.AzureStorage;
using LNUbiz.BLL.Interfaces.AzureStorage.Base;
using LNUbiz.BLL.Services.AzureStorage.Base;

namespace LNUbiz.BLL.Services.AzureStorage
{
    public class UserBlobStorageRepository : BlobStorageRepository, IUserBlobStorageRepository
    {
        private const string CONTAINER = "UserImages";
        public UserBlobStorageRepository(IAzureBlobConnectionFactory connectionFactory) : base(connectionFactory, CONTAINER)
        {
        }
    }
}
