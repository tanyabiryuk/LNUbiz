using LNUbiz.BLL.DTO.BusinessTripRequest;
using LNUbiz.BLL.ExtensionMethods;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNUbiz.BLL;
using LNUbiz.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace LNUbiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin + "," + Roles.User)]
    public class BusinessTripRequestController : ControllerBase
    {
        private readonly IBusinessTripRequestService                   _businessTripRequestService;
        private readonly ILoggerService<BusinessTripRequestController> _loggerService;
        private readonly UserManager<User>                             _userManager;
        private readonly IPdfService                                   _pdfService;

        public BusinessTripRequestController(
            IBusinessTripRequestService                   businessTripRequestService, 
            ILoggerService<BusinessTripRequestController> loggerService,
            UserManager<User>                             userManager,
            IPdfService                                   pdfService)
        {
            _businessTripRequestService = businessTripRequestService;
            _loggerService              = loggerService;
            _userManager                = userManager;
            _pdfService                 = pdfService;
        }

        /// <summary>
        /// Method to create new Business trip request.
        /// </summary>
        /// <param name="request">Business trip request model</param>
        /// <returns>Answer from backend</returns>
        /// <response code="201">Business trip request was successfully created</response>
        /// <response code="400">Request model is not valid</response>
        [HttpPost]
        public async Task<IActionResult> Create(BusinessTripRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                await _businessTripRequestService.CreateAsync(await _userManager.GetUserAsync(User), request);
                _loggerService.LogInformation($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                              $"created new business trip request");
                return StatusCode(StatusCodes.Status201Created, new { message =
                                              $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                              $"created new business trip request"});
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Method to get all existing Business trip requests
        /// </summary>
        /// <returns>List of business trip requests</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(StatusCodes.Status200OK, new { requests = 
                await _businessTripRequestService.GetAllAsync(await _userManager.GetUserAsync(User)) });
        }

        /// <summary>
        /// Method to get all existing Business trip requests that specified user has access to
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <returns>List of requests</returns>
        /// <response code="200">List of requests was successfully returned</response>
        /// <response code="404">The user with specified identification number does not exist</response>
        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId) ?? throw new NullReferenceException();
                return StatusCode(StatusCodes.Status200OK, new { 
                    requests = await _businessTripRequestService
                                     .GetAllAsync(userId) });
            }
            catch (NullReferenceException)
            {
                _loggerService.LogError($"The user (id: {userId}) was not found");
                return StatusCode(StatusCodes.Status404NotFound, new { message = 
                                              $"The user (id: {userId}) does not exist" });
            }
        }

        /// <summary>
        /// Method to get Business trip request.
        /// </summary>
        /// <param name="requestId">Request identification number</param>
        /// <returns>Business trip request</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="403">User hasn't access to specified request</response>
        /// <response code="404">The request does not exist</response>
        [HttpGet("{requestId:int}")]
        public async Task<IActionResult> Get([FromRoute] int requestId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { request = 
                    await _businessTripRequestService
                          .GetByIdAsync(await _userManager.GetUserAsync(User), requestId) });
            }
            catch (NullReferenceException)
            {
                _loggerService.LogError($"Business trip request (id: {requestId}) not found");
                return StatusCode(StatusCodes.Status404NotFound, new { message = 
                                              $"Business trip request (id: {requestId}) not found" });
            }
            catch (UnauthorizedAccessException)
            {
                _loggerService.LogError($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                        $"hasn't access to business trip request (id: {requestId})");
                return StatusCode(StatusCodes.Status403Forbidden, new { message = 
                                              $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                              $"hasn't access to business trip request (id: {requestId})"
                });
            }
        }

        /// <summary>
        ///  Returns pdf file as base64
        /// </summary>
        /// <param name="objId">Request identification number</param>
        /// <returns>Pdf file as base64 what was created with BusinesssTripRequest data</returns>
        /// <response code="200">Pdf file as base64</response>
        [HttpGet("createPdf/{objId:int}")]
        public async Task<IActionResult> CreatePdf(int objId)
        {
            var fileBytes = await _pdfService.BusinessTripRequestCreatePDFAsync(objId);
            var base64EncodedPdf = Convert.ToBase64String(fileBytes);

            return Ok(base64EncodedPdf);
        }

        /// <summary>
        /// Method to edit existing Business trip request.
        /// </summary>
        /// <param name="request">Business trip request model</param>
        /// <returns>Answer from backend</returns>
        /// <response code="200">Business trip request was successfully edited</response>
        /// <response code="400">Request model is not valid</response>
        /// <response code="403">User hasn't access to specified request</response>
        /// <response code="404">Business trip request does not exist</response>
        [HttpPut]
        public async Task<IActionResult> Edit(BusinessTripRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _businessTripRequestService.EditAsync(await _userManager.GetUserAsync(User), request);
                    _loggerService.LogInformation($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                                  $"edited business trip request (id: {request.Id})");
                    return StatusCode(StatusCodes.Status200OK, new { message = 
                                                  $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                                  $"edited business trip request (id: {request.Id})"
                    });
                }
                catch (InvalidOperationException)
                {
                    _loggerService.LogError($"Business trip request (id: {request.Id}) model is not valid");
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = $"Business trip request (id: {request.Id}) model is not valid"
                    });
                }
                catch (NullReferenceException)
                {
                    _loggerService.LogError($"Business trip request (id: {request.Id}) not found");
                    return StatusCode(StatusCodes.Status404NotFound, new 
                    { 
                        message = $"Business trip request (id: {request.Id}) not found" 
                    });
                }
                catch (UnauthorizedAccessException)
                {
                    _loggerService.LogError($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                            $"hasn't access to edit business trip request (id: {request.Id})");
                    return StatusCode(StatusCodes.Status403Forbidden, new 
                    { 
                        message = $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                  $"hasn't access to edit business trip request (id: {request.Id})" 
                    });
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Method to confirm business trip request
        /// </summary>
        /// <param name="requestId">Request identification number</param>
        /// <returns>Answer from backend</returns>
        /// <response code="200">The business trip request was successfully confirmed</response>
        /// <response code="404">The request does not exist</response>
        [HttpPut("confirm")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Confirm(int requestId)
        {
            try
            {
                await _businessTripRequestService.ConfirmAsync(await _userManager.GetUserAsync(User), requestId);
                _loggerService.LogInformation($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                              $"confirmed business trip request (id: {requestId})");
                return StatusCode(StatusCodes.Status200OK, new { message = 
                                              $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                              $"confirmed business trip request (id: {requestId})"
                });
            }
            catch (NullReferenceException)
            {
                _loggerService.LogError($"Business trip request (id: {requestId}) not found");
                return StatusCode(StatusCodes.Status404NotFound, new { message = 
                                              $"Business trip request (id: {requestId}) not found" });
            }
        }

        /// <summary>
        /// Method to cancel business trip request
        /// </summary>
        /// <param name="requestId">Request identification number</param>
        /// <returns>Answer from backend</returns>
        /// <response code="200">The business trip request was successfully canceled</response>
        /// <response code="404">The request does not exist</response>
        [HttpPut("cancel")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Cancel(int requestId)
        {
            try
            {
                await _businessTripRequestService.CancelAsync(await _userManager.GetUserAsync(User), requestId);
                _loggerService.LogInformation($"User (id: {(await _userManager.GetUserAsync(User)).Id})" +
                                              $" canceled business trip request (id: {requestId})");
                return StatusCode(StatusCodes.Status200OK, new { message = 
                                              $"User (id: {(await _userManager.GetUserAsync(User)).Id})" +
                                              $" canceled business trip request (id: {requestId})" });
            }
            catch (NullReferenceException)
            {
                _loggerService.LogError($"Business trip request (id: {requestId}) not found");
                return StatusCode(StatusCodes.Status404NotFound, new { message = 
                                              $"Business trip request (id: {requestId}) not found" });
            }
        }

        /// <summary>
        /// Method to delete business trip request
        /// </summary>
        /// <param name="id">Request identification number</param>
        /// <returns>Answer from backend</returns>
        /// <response code="200">The business trip request was successfully deleted</response>
        /// <response code="403">User hasn't access to business trip request</response>
        /// <response code="404">The business trip request does not exist</response>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _businessTripRequestService.DeleteAsync(await _userManager.GetUserAsync(User), id);
                _loggerService.LogInformation($"User (id: {(await _userManager.GetUserAsync(User)).Id})" +
                                              $" deleted business trip request (id: {id})");
                return StatusCode(StatusCodes.Status200OK, new { message = 
                                              $"User (id: {(await _userManager.GetUserAsync(User)).Id})" +
                                              $" deleted business trip request (id: {id})"
                });
            }
            catch (NullReferenceException)
            {
                _loggerService.LogError($"Business trip request (id: {id}) not found");
                return StatusCode(StatusCodes.Status404NotFound, new { message = 
                                              $"Business trip request (id: {id}) not found" });
            }
            catch (UnauthorizedAccessException)
            {
                _loggerService.LogError($"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                                        $"hasn't access to delete business trip request (id: {id})");
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = $"User (id: {(await _userManager.GetUserAsync(User)).Id}) " +
                              $"hasn't access to delete business trip request (id: {id})"
                });
            }
        }

        /// <summary>
        /// Method to get business trip request statuses
        /// </summary>
        /// <returns>List of enum values</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet("getStatuses")]
        public IActionResult GetStatuses()
        {
            var statuses = new List<string>();
            foreach (var enumValue in Enum.GetValues(typeof(BusinessTripRequestStatusDTO))
                                          .Cast<BusinessTripRequestStatusDTO>())
            {
                statuses.Add(enumValue.GetDescription());
            }
            return StatusCode(StatusCodes.Status200OK, new { statuses });
        }
    }
}
