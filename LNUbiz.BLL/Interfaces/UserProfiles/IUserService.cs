using LNUbiz.BLL.DTO;
using LNUbiz.BLL.DTO.UserProfiles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Interfaces.UserProfiles
{
    public interface IUserService
    {
        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>A user dto</returns>
        Task<UserDTO> GetUserAsync(string userId);

        /// <summary>
        /// Change a user
        /// </summary>
        /// <param name="user">User(dto) which needs to be changed</param>
        /// <param name="base64">Image in base64 format, which must be saved</param>
        /// <param name="placeOfWorkId">Id of "Work" which contains chosen place of work</param>
        /// <param name="positionId">Id of "Work" which contains chosen position</param>
        Task UpdateAsyncForBase64(UserDTO user, string base64, int? placeOfWorkId, int? positionId);

        /// <summary>
        /// Get a image
        /// </summary>
        /// <param name="fileName">The name of the image</param>
        /// <returns>Image in format base64</returns>
        Task<string> GetImageBase64Async(string fileName);
    }
}
