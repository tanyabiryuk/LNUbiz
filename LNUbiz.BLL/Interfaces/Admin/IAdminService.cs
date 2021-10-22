using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.UserProfiles;

namespace LNUbiz.BLL.Services.Interfaces
{
    public interface IAdminService
    {

        /// <summary>
        /// Change Current Role of user
        /// </summary>
        Task ChangeCurrentRoleAsync(string userId, string role);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userId">The id of the user, which must be deleted</param>
        Task DeleteUserAsync(string userId);

        /// <summary>
        /// Edit user roles
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="roles">List of new user roles</param>
        Task EditAsync(string userId, IEnumerable<string> roles);

        /// <summary>
        /// Get all roles except Admin role
        /// </summary>
        /// <returns>All roles except Admin role</returns>
        IEnumerable<IdentityRole> GetRolesExceptAdmin();

        Task<IEnumerable<UserDTO>> GetUsersAsync();
    }
}
