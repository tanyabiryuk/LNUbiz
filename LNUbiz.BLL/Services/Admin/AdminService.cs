using AutoMapper;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;
using LNUbiz.Resources;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.UserProfiles;

namespace LNUbiz.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminService(IRepositoryWrapper repoWrapper,
                            UserManager<User> userManager,
                            IMapper mapper,
                            RoleManager<IdentityRole> roleManager)
        {
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task ChangeCurrentRoleAsync(string userId, string role)
        {
            const string adminRole = Roles.Admin;
            const string userRole = Roles.User;
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            switch (role)
            {
                case adminRole:
                case userRole:
                    if (roles.Contains(adminRole))
                    {
                        await _userManager.RemoveFromRoleAsync(user, adminRole);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }
                    await _repoWrapper.SaveAsync();
                    await _userManager.AddToRoleAsync(user, role);
                    break;
            }
        }

        /// <inheritdoc />
        public async Task DeleteUserAsync(string userId)
        {
            User user = await _repoWrapper.User.GetFirstOrDefaultAsync(x => x.Id == userId);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null && !roles.Contains(Roles.Admin))
            {
                _repoWrapper.User.Delete(user);
                await _repoWrapper.SaveAsync();
            }
        }

        /// <inheritdoc />
        public async Task EditAsync(string userId, IEnumerable<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.
                Except(roles).
                Except(new List<string> { "Admin" });
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Count == 0)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
        }

        /// <inheritdoc />
        public IEnumerable<IdentityRole> GetRolesExceptAdmin()
        {
            var admin = _roleManager.Roles.Where(i => i.Name == Roles.Admin);
            var allRoles = _roleManager.Roles.Except(admin).OrderBy(i => i.Name);
            return allRoles;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var lowerRoles = new List<string>
            {
                Roles.User
            };
            var users = await _repoWrapper.User.GetAllAsync();
            var usersDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                var shortUser = _mapper.Map<User, UserDTO>(user);
                usersDtos.Add(shortUser);
            }
            return usersDtos;
        }
    }
}
