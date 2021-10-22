using System.Collections.Generic;

namespace LNUbiz.Resources
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "Користувач";

        
        public static List<string> ListOfRoles = new List<string>
        {
            Roles.Admin,
            Roles.User
        };
    }
}
