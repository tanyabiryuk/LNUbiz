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
                "lnubiz.manager@gmail.com",
                "Питання користувачів",
                $"<p>Контактні дані користувача :</p>" +
                $"<p>Електронна пошта {contactDTO.Email}, </p>" +
                $"<p>Ім'я {contactDTO.Name}, </p>" +
                $"<p>Телефон {contactDTO.PhoneNumber}  </p>" +
                $"<p>Опис питання : </p>" +
                $"<p>{contactDTO.FeedBackDescription} </p>",
                contactDTO.Email);
        }
    }
}
