using System.Threading.Tasks;
using LNUbiz.BLL.DTO.Account;
using LNUbiz.BLL.Interfaces;

namespace LNUbiz.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly IEmailSendingService _emailSendingService;

        public HomeService(IEmailSendingService emailSendingService)
        {
            _emailSendingService = emailSendingService;
        }

        public Task SendEmailAdmin(ContactsDto contactDTO)
        {
            return _emailSendingService.SendEmailAsync(
                "LNUbiz.admin@lnu.edu.ua",
                "Питання користувачів",
                $"Контактні дані користувача : Електронна пошта {contactDTO.Email}, " +
                $"Ім'я {contactDTO.Name}," +
                $"Телефон {contactDTO.PhoneNumber}  " +
                $"Опис питання : {contactDTO.FeedBackDescription}",
                contactDTO.Email);
        }
    }
}
