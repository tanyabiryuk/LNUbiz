using LNUbiz.BLL.DTO.UserProfiles;
using System.Collections.Generic;

namespace LNUbiz.Web.Models.UserModels
{
    public class EditUserViewModel
    {
        public UserViewModel User { get; set; }
        public string ImageBase64 { get; set; }
    }
}
