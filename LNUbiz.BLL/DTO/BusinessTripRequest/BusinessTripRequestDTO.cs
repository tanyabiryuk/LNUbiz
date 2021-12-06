using System;
using System.ComponentModel.DataAnnotations;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.DTO.BusinessTripRequest
{
    public class BusinessTripRequestDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime Date { get; set; }

        public BusinessTripRequestStatusDTO Status { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(200, ErrorMessage = "Максимально допустима кількість символів 200")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26})*$",
            ErrorMessage = "Поле має містити тільки літери")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(400, ErrorMessage = "Максимально допустима кількість символів 400")]
        public string FullTimePosition { get; set; }

        [MaxLength(400, ErrorMessage = "Максимально допустима кількість символів 400")]
        public string PartTimePosition { get; set; }

        public bool IsAbroadTrip { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(1000, ErrorMessage = "Максимально допустима кількість символів 1000")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "Оберіть відповідь")]
        public PayRetentionType RetentionType { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(200, ErrorMessage = "Максимально допустима кількість символів 200")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26})*$",
            ErrorMessage = "Поле має містити тільки літери")]
        public string City { get; set; }

        [MaxLength(200, ErrorMessage = "Максимально допустима кількість символів 200")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ'.`]{1,26})*$",
            ErrorMessage = "Поле має містити тільки літери")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(400, ErrorMessage = "Максимально допустима кількість символів 400")]
        public string Institution { get; set; }

        [Required(ErrorMessage = "Дата початку відрядження не заповнена.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Дата завершення відрядження не заповнена.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EndDate { get; set; }

        [MaxLength(1000, ErrorMessage = "Максимально допустима кількість символів 1000")]
        public string Route { get; set; }

        [MaxLength(1000, ErrorMessage = "Максимально допустима кількість символів 1000")]
        public string Transport { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(1000, ErrorMessage = "Максимально допустима кількість символів 1000")]
        public string ExpensesPayment { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [MaxLength(1000, ErrorMessage = "Максимально допустима кількість символів 1000")]
        public string TripReason { get; set; }
    }
}