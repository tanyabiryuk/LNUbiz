using LNUbiz.BLL.DTO.BusinessTripRequest;
using System.Collections.Generic;
using System.Threading.Tasks;
using LNUbiz.DAL.Entities;

namespace LNUbiz.BLL.Services.Interfaces
{
    public interface IBusinessTripRequestService
    {
        /// <summary>
        /// Method to get all the information in the BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="id">BusinessTrip request identification number</param>
        /// <returns>BusinessTrip request model</returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to BusinessTrip request</exception>
        /// <exception cref="System.NullReferenceException">Thrown when BusinessTrip request doesn't exist</exception>
        Task<BusinessTripRequestDTO> GetByIdAsync(User user, int id);

        /// <summary>
        /// Method to get all requests that the userId has access to
        /// </summary>
        /// <param name="userId">Authorized user id</param>
        /// <returns>List of BusinessTrip request models</returns>
        Task<IEnumerable<BusinessTripRequestDTO>> GetAllAsync(string userId);


        /// <summary>
        /// Method to get all requests that the user has access to
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <returns>List of BusinessTrip request models</returns>
        Task<IEnumerable<BusinessTripRequestDTO>> GetAllAsync(User user);

        /// <summary>
        /// Method to create new BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="businessTripRequestDTO">BusinessTrip request model</param>
        Task CreateAsync(User user, BusinessTripRequestDTO businessTripRequestDTO);

        /// <summary>
        /// Method to edit BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="businessTripRequestDTO">BusinessTrip request model</param>
        /// <exception cref="System.InvalidOperationException">Thrown when BusinessTrip request can not be edited</exception>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to BusinessTrip request</exception>
        /// <exception cref="System.NullReferenceException">Thrown when BusinessTrip request doesn't exist</exception>
        Task EditAsync(User user, BusinessTripRequestDTO businessTripRequestDTO);

        /// <summary>
        /// Method to confirm BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="id">BusinessTrip request identification number</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to BusinessTrip request</exception>
        /// <exception cref="System.NullReferenceException">Thrown when BusinessTrip request doesn't exist</exception>
        Task ConfirmAsync(User user, int id);

        /// <summary>
        /// Method to cancel BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="id">BusinessTrip request identification number</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to BusinessTrip request</exception>
        /// <exception cref="System.NullReferenceException">Thrown when BusinessTrip request doesn't exist</exception>
        Task CancelAsync(User user, int id);

        /// <summary>
        /// Method to delete BusinessTrip request
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <param name="id">BusinessTrip request identification number</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to BusinessTrip request</exception>
        /// <exception cref="System.NullReferenceException">Thrown when BusinessTrip request doesn't exist</exception>
        Task DeleteAsync(User user, int id);
    }
}