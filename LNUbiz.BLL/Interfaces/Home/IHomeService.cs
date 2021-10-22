﻿using LNUbiz.BLL.DTO.Account;
using System.Threading.Tasks;

namespace LNUbiz.BLL.Interfaces
{
    public interface IHomeService
    {
        /// <summary>
        /// Sending message to the administration
        /// </summary>
        /// <param name="contactDTO"></param>
        /// <returns>Result of sending email to the administration</returns>
        Task SendEmailAdmin(ContactsDto contactDTO);
    }
}
