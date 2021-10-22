using System;
using System.ComponentModel.DataAnnotations;


namespace LNUbiz.DAL.Entities
{
    public class ConfirmedUser
    {
        public int ID { get; set; }
        [Required]
        public User User { get; set; }
        public string UserID { get; set; }
        public DateTime ConfirmDate { get; set; }

    }
}
