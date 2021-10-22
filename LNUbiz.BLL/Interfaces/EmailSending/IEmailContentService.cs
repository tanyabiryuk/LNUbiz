using LNUbiz.BLL.Models;
using LNUbiz.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace LNUbiz.BLL.Interfaces
{
    /// <summary>
    /// Returns emails contents needed for LNUbiz
    /// </summary>
    public interface IEmailContentService
    {
        /// <summary>
        /// Get email for Google registration
        /// </summary>
        /// <returns>Email content</returns>
        EmailModel GetAuthGoogleRegisterEmail();

        /// <summary>
        /// Get email for registration greeting
        /// </summary>
        /// <returns>Email content</returns>
        EmailModel GetAuthGreetingEmail();

        /// <summary>
        /// Get email for registration confirmation
        /// </summary>
        /// <param name="confirmationLink">Registration confirmation link</param>
        /// <returns>Email content</returns>
        EmailModel GetAuthRegisterEmail(string confirmationLink);

        /// <summary>
        /// Get email for password resetting
        /// </summary>
        /// <param name="confirmationLink">Password resetting confirmation link</param>
        /// <returns>Email content</returns>
        EmailModel GetAuthResetPasswordEmail(string confirmationLink);

        /// <summary>
        /// Get email to inform user about exclude from LNUbiz
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Email content</returns>
        Task<EmailModel> GetExcludeEmailAsync(string userId);
    }
}
