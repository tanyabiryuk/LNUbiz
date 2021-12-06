using AutoMapper;
using LNUbiz.BLL.DTO.UserProfiles;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Interfaces.UserProfiles;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.Web.Models.UserModels;
using LNUbiz.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LNUbiz.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LNUbiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService                   _userService;
        private readonly IUserManagerService            _userManagerService;
        private readonly ILoggerService<UserController> _loggerService;
        private readonly IMapper                        _mapper;
        private readonly UserManager<User>              _userManager;

        public UserController(IUserService userService
                            , IUserManagerService userManagerService
                            , ILoggerService<UserController> loggerService
                            , IMapper mapper
                            , UserManager<User> userManager)
        {
            _userService        = userService;
            _userManagerService = userManagerService;
            _loggerService      = loggerService;
            _mapper             = mapper;
            _userManager        = userManager;
        }


        /// <summary>
        /// Get a specify user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>A user</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User not found</response>
        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _loggerService.LogError("User id is null");
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);
            var currentUser = await _userService.GetUserAsync(currentUserId);
            var user = await _userService.GetUserAsync(userId);
            if (user != null)
            {
                var isThisUser = currentUserId == userId;

                if (await _userManagerService.IsInRoleAsync(currentUser, Roles.User) && !isThisUser)
                {
                    _loggerService.LogError($"User (id: {currentUserId}) hasn't access to profile (id: {userId})");
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                return Ok(_mapper.Map<UserDTO, UserViewModel>(user));
            }

            _loggerService.LogError($"User not found. UserId:{userId}");
            return NotFound();
        }

        /// <summary>
        /// Get a specify user profile
        /// </summary>
        /// <param name="focusUserId">The id of the focus user</param>
        /// <param name="currentUserId">The id of the current user</param>
        /// <returns>A focus user profile</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Focus user not found</response>
        [HttpGet("{currentUserId}/{focusUserId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserProfile(string currentUserId, string focusUserId)
        {
            if (string.IsNullOrEmpty(focusUserId))
            {
                _loggerService.LogError("User id is null");
                return NotFound();
            }
            var currentUser = await _userService.GetUserAsync(currentUserId);
            var focusUser = await _userService.GetUserAsync(focusUserId);

            if (focusUser == null)
            {
                _loggerService.LogError($"User not found. UserId:{focusUserId}");
                return NotFound();
            }
            var isThisUser = currentUserId == focusUserId;
            var isUserAdmin = await _userManagerService.IsInRoleAsync(currentUser, Roles.Admin);
            var isFocusUserAdmin = await _userManagerService.IsInRoleAsync(focusUser, Roles.Admin);
            if (!isUserAdmin && !isThisUser)
            {
                _loggerService.LogError($"User (id: {currentUserId}) hasn't access to profile (id: {focusUserId})");
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var model = _mapper.Map<UserDTO, UserViewModel>(focusUser);

            return Ok(model);

        }

        /// <summary>
        /// Get a image
        /// </summary>
        /// <param name="imageName">The name of the image</param>
        /// <returns>Image in format base64</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet("getImage/{imageName}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            return Ok(await _userService.GetImageBase64Async(imageName));
        }
    }
}
