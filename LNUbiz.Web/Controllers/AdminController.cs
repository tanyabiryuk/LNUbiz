using LNUbiz.BLL.DTO.Admin;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.Resources;
using LNUbiz.Web.Models.Admin;
using LNUbiz.Web.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LNUbiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        private readonly ILoggerService<AdminController> _loggerService;

        private readonly IUserManagerService _userManagerService;

        public AdminController(ILoggerService<AdminController> logger,
                                                    IUserManagerService userManagerService,
            IAdminService adminService)
        {
            _loggerService = logger;
            _userManagerService = userManagerService;
            _adminService = adminService;
        }

        /// <summary>
        /// Change current user role
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="role">The new current role of user</param>
        /// <response code="201">Successful operation</response>
        /// <response code="404">User not found</response>
        [HttpPut("changeRole/{userId}/{role}")]
        public async Task<IActionResult> ChangeCurrentUserRole(string userId, string role)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _adminService.ChangeCurrentRoleAsync(userId, role);
                _loggerService.LogInformation($"Successful change role for {userId}");
                return NoContent();
            }
            _loggerService.LogError("User id is null");
            return NotFound();
        }

        /// <summary>
        /// Confirmation of delete a user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The id of the user</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User id is null</response>
        [HttpGet("confirmDelete/{userId}")]
        [Authorize(Roles = Roles.Admin)]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _loggerService.LogError("User id is null");
                return BadRequest();
            }
            return Ok(userId);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userId">The id of the user, which must be deleted</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User id is null</response>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _adminService.DeleteUserAsync(userId);
                _loggerService.LogInformation($"Successful delete user {userId}");

                return Ok();
            }
            _loggerService.LogError("User id is null");
            return BadRequest();
        }

        /// <summary>
        /// Get specify model for edit roles for selected user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>A data of roles for editing user roles</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User not found</response>
        [HttpGet("editRole/{userId}")]
        public async Task<IActionResult> Edit(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManagerService.FindByIdAsync(userId);
                if (user == null)
                {
                    _loggerService.LogError("User id is null");
                    return NotFound();
                }
                var userRoles = await _userManagerService.GetRolesAsync(user);
                var allRoles = _adminService.GetRolesExceptAdmin();

                RoleViewModel model = new RoleViewModel
                {
                    UserID = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return Ok(model);
            }
            _loggerService.LogError("User id is null");
            return NotFound();
        }

        /// <summary>
        /// Edit user roles
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="roles">List of new user roles</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User not found</response>
        [HttpPut("editedRole/{userId}")]
        public async Task<IActionResult> Edit(string userId, [FromBody] List<string> roles)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _adminService.EditAsync(userId, roles);
                _loggerService.LogInformation($"Successful change role for {userId}");
                return Ok();
            }
            _loggerService.LogError("User id is null");
            return NotFound();
        }

        /// <summary>
        /// Get all users with additional information
        /// </summary>
        /// <returns>Specify model with all users</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet("usersTable")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _adminService.GetUsersAsync());
        }
    }
}
