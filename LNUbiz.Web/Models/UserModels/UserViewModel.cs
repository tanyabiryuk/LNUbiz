using System;
using System.ComponentModel.DataAnnotations;

namespace LNUbiz.Web.Models.UserModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        [Display(Name = "Ім'я")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ'().`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ'().`]{1,26})*$",
            ErrorMessage = "Поле 'Ім'я' має містити тільки літери")]
        [Required(ErrorMessage = "Поле 'Ім'я' є обов'язковим")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Поле 'Ім'я' повинне складати від 2 до 25 символів")]
        public string FirstName { get; set; }
        [Display(Name = "Прізвище")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ'().`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ'().`]{1,26})*$",
            ErrorMessage = "Поле 'Прізвище' має містити тільки літери")]
        [Required(ErrorMessage = "Поле 'Прізвище' є обов'язковим")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Поле 'Прізвище' повинне складати від 2 до 25 символів")]
        public string LastName { get; set; }
        [Display(Name = "По-батькові")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ()'.`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ()'.`]{1,26})*$",
            ErrorMessage = "Поле 'По-батькові' має містити тільки літери")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Поле по-батькові повинне складати від 2 до 25 символів")]
        public string FatherName { get; set; }
        [StringLength(18, MinimumLength = 10, ErrorMessage = "Номер телефону повинен містити 10 цифр")]
        [Required(ErrorMessage = "Поле 'Номер телефону' є обов'язковим")]
        public string PhoneNumber { get; set; }
        public DateTime RegistredOn { get; set; }
        public DateTime EmailSendedOnRegister { get; set; }
        public DateTime EmailSendedOnForgotPassword { get; set; }
        public string ImagePath { get; set; }
    }
}
