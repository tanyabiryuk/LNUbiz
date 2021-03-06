using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.Account;
using LNUbiz.BLL.Interfaces;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Services.Auth
{
    public class AuthEmailService : IAuthEmailService
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IAuthService           _authService;
        private readonly IHttpContextAccessor   _contextAccessor;
        private readonly IEmailSendingService   _emailSendingService;
        private readonly IEmailContentService   _emailContentService;
        private readonly IUrlHelperFactory      _urlHelperFactory;
        private readonly UserManager<User>      _userManager;

        public AuthEmailService(IEmailSendingService   emailSendingService,
                                IEmailContentService   emailContentService,
                                IAuthService           authService,
                                UserManager<User>      userManager,
                                IUrlHelperFactory      urlHelperFactory,
                                IActionContextAccessor actionContextAccessor,
                                IHttpContextAccessor   contextAccessor)
        {
            _emailSendingService = emailSendingService;
            _emailContentService = emailContentService;
            _authService         = authService;
            _userManager         = userManager;

            _contextAccessor       = contextAccessor;
            _urlHelperFactory      = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        /// <inheritdoc />
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var user   = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> SendEmailGreetingAsync(string email)
        {
            var user         = await _userManager.FindByEmailAsync(email);
            var emailContent = _emailContentService.GetAuthGreetingEmail();
            return await _emailSendingService
                             .SendEmailAsync( user.Email
                                            , emailContent.Subject
                                            , emailContent.Message
                                            , emailContent.Title);
        }

        /// <inheritdoc />
        public async Task<bool> SendEmailRegistrAsync(string email)
        {
            IUrlHelper _url      = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var token            = await _authService.AddRoleAndTokenAsync(email);
            var user             = await _userManager.FindByEmailAsync(email);
            var confirmationLink = _url.Action("confirmingEmail",
                                               "Auth",
                                               new { token, userId = user.Id },
                                               _contextAccessor.HttpContext.Request.Scheme);
            user.EmailSendedOnRegister = DateTime.Now;
            var emailContent           = _emailContentService.GetAuthRegisterEmail(confirmationLink);
            return await _emailSendingService
                             .SendEmailAsync( email
                                            , emailContent.Subject
                                            , emailContent.Message
                                            , emailContent.Title)
                 ? (await _userManager.UpdateAsync(user)).Succeeded 
                 : false;
        }

        /// <inheritdoc />
        public async Task SendEmailResetingAsync(string confirmationLink, ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            user.EmailSendedOnForgotPassword = DateTime.Now;
            await _userManager.UpdateAsync(user);
            var emailContent = _emailContentService.GetAuthResetPasswordEmail(confirmationLink);
            await _emailSendingService.SendEmailAsync( forgotPasswordDto.Email
                                                     , emailContent.Subject
                                                     , emailContent.Message
                                                     , emailContent.Title);
        }
    }
}
