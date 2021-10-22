using LNUbiz.BLL.DTO;
using System.Collections.Generic;
using LNUbiz.BLL.DTO.UserProfiles;

namespace LNUbiz.Web.Models.Admin
{
    public class AdminTypeViewModel
    {
        public int ID { get; set; }
        public string AdminTypeName { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
        public int Total { get; set; }
    }
}
