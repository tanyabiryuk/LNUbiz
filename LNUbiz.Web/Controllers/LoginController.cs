using LNUbiz.BLL.DTO.Account;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.Jwt;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Interfaces.Resources;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LNUbiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService                    _authService;
        private readonly IJwtService                     _jwtService;
        private readonly ILoggerService<LoginController> _loggerService;
        private readonly IResources                      _resources;

        public LoginController(IAuthService                    authService
                             , IResources                      resources
                             , IJwtService                     jwtService
                             , ILoggerService<LoginController> loggerService
                             , IUserManagerService             userManagerService
                             , UserManager<User>               userManager)
        {
            _authService   = authService;
            _resources     = resources;
            _jwtService    = jwtService;
            _loggerService = loggerService;
        }

        [HttpGet("GoogleClientId")]
        [AllowAnonymous]
        public IActionResult GetGoogleClientId()
        {
            return Ok(new { id = ConfigSettingLayoutRenderer.DefaultConfiguration
                                                            .GetSection("GoogleAuthentication")["GoogleClientId"] });
        }

        /// <summary>
        /// Method for logining in system with Google account
        /// </summary>
        /// <param name="googleToken">Login model(dto)</param>
        /// <returns>Answer from backend for login method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with logining</response>
        [HttpPost("signin/google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin(string googleToken)
        {
            try
            {
                var user = await _authService.GetGoogleUserAsync(googleToken);
                var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                return Ok(new { token = generatedToken });
            }
            catch (NullReferenceException)
            {
                return BadRequest(_resources.ResourceForErrors["Login-NotRegistered"]);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(_resources.ResourceForErrors["Login-NotConfirmed"]);
            }
            catch (Exception exc)
            {
                _loggerService.LogError(exc.Message);
            }
            return BadRequest();
        }

        /// <summary>
        /// Method for logining in system
        /// </summary>
        /// <param name="loginDto">Login model(dto)</param>
        /// <returns>Answer from backend for login method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with logining</response>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest(_resources.ResourceForErrors["Login-NotRegistered"]);
                }
                if (!await _authService.IsEmailConfirmedAsync(user))
                {
                    return BadRequest(_resources.ResourceForErrors["Login-NotConfirmed"]);
                }
                var result = await _authService.SignInAsync(loginDto);
                if (result.IsLockedOut)
                {
                    return BadRequest(_resources.ResourceForErrors["Account-Locked"]);
                }
                if (result.Succeeded)
                {
                    var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                    return Ok(new { token = generatedToken });
                }
                return BadRequest(_resources.ResourceForErrors["Login-InCorrectPassword"]);
            }
            return Ok(_resources.ResourceForErrors["ModelIsNotValid"]);
        }

        [HttpGet("logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Logout()
        {
            _authService.SignOutAsync();
            return Ok();
        }
    }
}
