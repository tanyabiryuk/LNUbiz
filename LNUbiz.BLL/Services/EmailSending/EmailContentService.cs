using System.Threading.Tasks;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.UserProfiles;
using LNUbiz.BLL.Models;

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
                Title   = "Адміністрація сайту LNUbiz",
                Subject = "Повідомлення про реєстрацію",
                Message = "Ви зареєструвались в системі LNUbiz використовуючи свій Google-акаунт. "
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthGreetingEmail()
        {
            return new EmailModel
            {
                Title   = "LNUbiz",
                Subject = "Вітаємо у системі!",
                Message = $"<p>Ви успішно активували свій акаунт!</p> " +
                          $"<br/>" +
                          $"<p><i>Адміністрація сайту LNUbiz.</i></p>"
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthRegisterEmail(string confirmationLink)
        {
            return new EmailModel
            {
                Title   = "LNUbiz",
                Subject = "Підтвердження реєстрації",
                Message = $"Підтвердіть реєстрацію, перейшовши за <a href='{confirmationLink}'>посиланням</a>"
            };
        }

        /// <inheritdoc />
        public EmailModel GetAuthResetPasswordEmail(string confirmationLink)
        {
            return new EmailModel
            {
                Title   = "Адміністрація сайту LNUbiz",
                Subject = "Скидання пароля",
                Message = $"Для скидання пароля перейдіть за <a href='{confirmationLink}'>посиланням</a>"
            };
        }

        /// <inheritdoc />
        public async Task<EmailModel> GetExcludeEmailAsync(string userId)
        {
            return new EmailModel
            {
                Title   = "LNUbiz",
                Subject = "Вилучення із системи",
                Message = $"<p>Повідомляємо, що Вас було вилучено із системи LNUbiz.!</p> " +
                          $"<br/>" +
                          $"<p><i>Адміністрація сайту LNUbiz.</i></p>"
            };
        }

    }
}
