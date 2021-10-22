using System.ComponentModel.DataAnnotations;

namespace LNUbiz.DAL.Entities
{
    public class AdminType
    {
        public int ID { get; set; }

        [Required, MaxLength(50, ErrorMessage = "AdminTypeName name cannot exceed 50 characters")]
        public string AdminTypeName { get; set; }
    }
}
