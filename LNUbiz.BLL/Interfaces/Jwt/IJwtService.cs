using LNUbiz.BLL.DTO.UserProfiles;
using System.Threading.Tasks;

namespace LNUbiz.BLL.Interfaces.Jwt
{
    public interface IJwtService
    {
        /// <summary>
        /// Generting JWT token
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Returns generated JWT token</returns>
        Task<string> GenerateJWTTokenAsync(UserDTO userDTO);
    }
}
