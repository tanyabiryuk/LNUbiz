using System;
using System.Threading.Tasks;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.UserProfiles;
using LNUbiz.BLL.Models;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Services.EmailSending
{
    public class EmailContentService : IEmailContentService
    {
        private readonly IUserService _userService;

        public EmailContentService(IUserService userService)
        {
            _userService = userService;
        }

        /// <inheritdoc />
        public EmailModel GetAuthGoogleRegisterEmail()
        {
            return new EmailModel
            {
                Title = "Адміністрація сайту LNUbiz",
                Subject = "Повідомлення про реєстрацію",
                Message = "Ви зареєструвались в системі LNUbiz використовуючи свій Google-акаунт. "
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthGreetingEmail()
        {
            return new EmailModel
            {
                Title = "LNUbiz",
                Subject = "Вітаємо у системі!",
                Message = $"Ви успішно активували свій акаунт!\n\nАдміністрація сайту LNUbiz."
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthRegisterEmail(string confirmationLink)
        {
            return new EmailModel
            {
                Title = "LNUbiz",
                Subject = "Підтвердження реєстрації",
                Message = $"Підтвердіть реєстрацію, перейшовши за <a href='{confirmationLink}'>посиланням</a>"
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthResetPasswordEmail(string confirmationLink)
        {
            return new EmailModel
            {
                Title = "Адміністрація сайту LNUbiz",
                Subject = "Скидання пароля",
                Message = $"Для скидання пароля перейдіть за <a href='{confirmationLink}'>посиланням</a>"
            };
        }

        /// <inheritdoc />
        public async Task<EmailModel> GetExcludeEmailAsync(string userId)
        {
            return new EmailModel
            {
                Title = "LNUbiz",
                Subject = "Вилечення із системи",
                Message = "<h3>СКОБ!</h3>"
                          + $"<p>Повідомляємо, що Вас було виключено із системи LNUbiz.\n\nАдміністрація сайту LNUbiz."
            };
        }

    }
}
