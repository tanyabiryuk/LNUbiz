using LNUbiz.BLL.DTO.Account;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Interfaces.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LNUbiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthEmailService               _authEmailServices;
        private readonly ILoggerService<LoginController> _loggerService;
        private readonly IAuthService                    _authService;
        private readonly IHomeService                    _homeService;
        private readonly IResources                      _resources;

        public AuthController(IAuthEmailService               authEmailServices
                            , ILoggerService<LoginController> loggerService
                            , IAuthService                    authService
                            , IHomeService                    homeService
                            , IResources                      resources)
        {
            _authEmailServices = authEmailServices;
            _loggerService     = loggerService;
            _authService       = authService;
            _homeService       = homeService;
            _resources         = resources;
        }

        /// <summary>
        /// Method for confirming email in system
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="token">Token for confirming email</param>
        /// <returns>Answer from backend for confirming email method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with confirming email</response>
        [HttpGet("confirmingEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmingEmailAsync(string userId, string token)
        {
            var userDto = await _authService.FindByIdAsync(userId);
            if (userDto == null)
            {
                return BadRequest();
            }
            int totalTime = _authService.GetTimeAfterRegistr(userDto);
            if (totalTime < 1440)
            {
                if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(token))
                {
                    return BadRequest();
                }
                var result = await _authEmailServices.ConfirmEmailAsync(userDto.Id, token);

                if (result.Succeeded)
                {
                    string signinurl = ConfigSettingLayoutRenderer.DefaultConfiguration.GetSection("URLs")["SignIn"];
                    var greetingSendResult = await _authEmailServices.SendEmailGreetingAsync(userDto.Email);
                    if (greetingSendResult)
                        return Redirect(signinurl);
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Ok(_resources.ResourceForErrors["ConfirmedEmailNotAllowed"]);
            }
        }

        /// <summary>
        /// Method for registering in system
        /// </summary>
        /// <param name="registerDto">Register model(dto)</param>
        /// <returns>Answer from backend for register method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with registration</response>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(_resources.ResourceForErrors["Register-InCorrectData"]);
            }
            var registeredUser = await _authService.FindByEmailAsync(registerDto.Email);
            if (registeredUser != null)
            {
                return BadRequest(_resources.ResourceForErrors["Register-RegisteredUser"]);
            }
            else
            {
                var result = await _authService.CreateUserAsync(registerDto);
                if (!result.Succeeded)
                {
                    return BadRequest(_resources.ResourceForErrors["Register-InCorrectPassword"]);
                }
                else
                {
                    if (!(await _authEmailServices.SendEmailRegistrAsync(registerDto.Email)))
                    {
                        return BadRequest(_resources.ResourceForErrors["Register-SMTPServerError"]);
                    }
                    return Ok(_resources.ResourceForErrors["Confirm-Registration"]);
                }
            }
        }

        /// <summary>
        /// Method for registering in system with Google account
        /// </summary>
        /// <param name="googleToken">Register model(dto)</param>
        /// <returns>Answer from backend for register method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with registration</response>
        [HttpPost("signup/google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleSignUp(string googleToken)
        {
            try
            {
                var user = await _authService.CreateGoogleUserAsync(googleToken);
                if (user == null)
                {
                    return BadRequest();
                }
                if (!(await _authEmailServices.SendEmailRegistrAsync(user.Email)))
                {
                    return BadRequest(_resources.ResourceForErrors["Register-SMTPServerError"]);
                }
                return Ok(_resources.ResourceForErrors["Confirm-Registration"]);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(_resources.ResourceForErrors["Register-RegisteredUser"]);
            }
            catch (Exception exc)
            {
                _loggerService.LogError(exc.Message);
            }
            return BadRequest();
        }

        /// <summary>
        /// Method for resending email after SMTPServer error
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>Answer from backend for resending email method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with resending email</response>
        [HttpPost("resendEmailForRegistering/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendEmailForRegistering(string userId)
        {
            var userDto = await _authService.FindByIdAsync(userId);
            if (userDto == null)
            {
                return BadRequest();
            }
            await _authEmailServices.SendEmailRegistrAsync(userDto.Email);
            return Ok(_resources.ResourceForErrors["EmailForRegistering-Resended"]);
        }

        /// <summary>
        /// Method for sending question to Admin in system
        /// </summary>
        /// <param name="contactsDto">Contacts model(dto)</param>
        /// <returns>Answer from backend sending question to Admin in system</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Problems with sending question</response>
        [HttpPost("sendQuestion")]
        public async Task<IActionResult> SendContacts([FromBody] ContactsDto contactsDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Дані введені неправильно");
                return BadRequest(_resources.ResourceForErrors["ModelIsNotValid"]);
            }
            await _homeService.SendEmailAdmin(contactsDto);

            return Ok(_resources.ResourceForErrors["Feedback-Sended"]);
        }
    }
}
