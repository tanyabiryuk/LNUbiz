using AutoMapper;
using LNUbiz.BLL.DTO.UserProfiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.AzureStorage;
using LNUbiz.BLL.Interfaces.UserProfiles;
using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;

namespace LNUbiz.BLL.Services.UserProfiles
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IUserBlobStorageRepository _userBlobStorage;
        private readonly IUniqueIdService _uniqueId;

        public UserService(IRepositoryWrapper repoWrapper,
            IMapper mapper,
            IUserBlobStorageRepository userBlobStorage,
            IWebHostEnvironment env,
            IUniqueIdService uniqueId)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _userBlobStorage = userBlobStorage;
            _env = env;
            _uniqueId = uniqueId;
        }

        /// <inheritdoc />
        public async Task<UserDTO> GetUserAsync(string userId)
        {
            var user = await _repoWrapper.User.GetFirstAsync(
                i => i.Id == userId,
                i =>
                    i.Include(g => g.BusinessTripRequests));
            var model = _mapper.Map<User, UserDTO>(user);

            return model;
        }

        /// <inheritdoc />
        public async Task UpdateAsyncForBase64(UserDTO user, string base64, int? placeOfWorkId, int? positionId)
        {
            user.ImagePath ??= await UploadPhotoAsyncFromBase64(user.Id, base64);
            await UpdateAsync(user, base64, placeOfWorkId, positionId);
            await _repoWrapper.SaveAsync();
        }

        /// <inheritdoc />
        public async Task<string> GetImageBase64Async(string fileName)
        {
            return await _userBlobStorage.GetBlobBase64Async(fileName);
        }

        private async Task<string> UploadPhotoAsyncFromBase64(string userId, string imageBase64)
        {
            var oldImageName = (await _repoWrapper.User.GetFirstOrDefaultAsync(x => x.Id == userId)).ImagePath;
            if (!string.IsNullOrWhiteSpace(imageBase64) && imageBase64.Length > 0)
            {
                var base64Parts = imageBase64.Split(',');
                var ext = base64Parts[0].Split(new[] { '/', ';' }, 3)[1];
                var fileName = $"{_uniqueId.GetUniqueId()}.{ext}";
                await _userBlobStorage.UploadBlobForBase64Async(base64Parts[1], fileName);
                if (!string.IsNullOrEmpty(oldImageName) && !string.Equals(oldImageName, "default_user_image.png"))
                {
                    await _userBlobStorage.DeleteBlobAsync(oldImageName);
                }

                return fileName;
            }
            else
            {
                return oldImageName;
            }
        }

        /// <inheritdoc />
        private async Task UpdateAsync(UserDTO user, string base64, int? placeOfWorkId, int? positionId)
        {
            var userForUpdate = _mapper.Map<UserDTO, User>(user);
            _repoWrapper.User.Update(userForUpdate);
            await _repoWrapper.SaveAsync();
        }
        

        private string SaveCorrectLink(string link, string socialMediaName)
        {
            if (link != null && link != "")
            {
                if (link.Contains($"www.{socialMediaName}.com/"))
                {
                    if (link.Contains("https://"))
                    {
                        link = link.Substring(8);
                    }
                    link = link.Substring(socialMediaName.Length + 9);
                }
                else if (link.Contains($"{socialMediaName}.com/"))
                {
                    if (link.Contains("https://"))
                    {
                        link = link.Substring(8);
                    }
                    link = link.Substring(socialMediaName.Length + 5);
                }
                return link;
            }
            return link;
        }
    }
}
