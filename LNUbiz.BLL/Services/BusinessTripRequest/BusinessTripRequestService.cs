using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.BusinessTripRequest;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;

namespace LNUbiz.BLL.Services
{
    public class BusinessTripRequestService : IBusinessTripRequestService
    {
        private readonly IRepositoryWrapper                _repositoryWrapper;
        private readonly IBusinessTripRequestAccessService _requestAccessService;
        private readonly IMapper                           _mapper;

        public BusinessTripRequestService(IRepositoryWrapper repositoryWrapper
                                        , IBusinessTripRequestAccessService requestAccessService
                                        , IMapper mapper)
        {
            _repositoryWrapper    = repositoryWrapper;
            _requestAccessService = requestAccessService;
            _mapper               = mapper;
        }

        ///<inheritdoc/>
        public async Task<BusinessTripRequestDTO> GetByIdAsync(User user, int id)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                    predicate: r => r.Id == id,
                    include: source => source
                        .Include(r => r.User)) ?? throw new NullReferenceException();
            if (!await _requestAccessService.HasAccessAsync(user, id))
            {
                throw new UnauthorizedAccessException();
            }
            return _mapper.Map<BusinessTripRequest, BusinessTripRequestDTO>(request);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<BusinessTripRequestDTO>> GetAllAsync(User user)
        {
            return await _requestAccessService.GetRequestsAsync(user);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<BusinessTripRequestDTO>> GetAllAsync(string userId)
        {
            var requests = await _repositoryWrapper.BusinessTripRequests.GetAllAsync(
                predicate: r => r.UserId == userId);
            return _mapper.Map<IEnumerable<BusinessTripRequest>,
                               IEnumerable<BusinessTripRequestDTO>>(requests);
        }

        ///<inheritdoc/>
        public async Task CreateAsync(User user, BusinessTripRequestDTO requestDto)
        {
            var request    = _mapper.Map<BusinessTripRequestDTO, BusinessTripRequest>(requestDto);
            request.UserId = user.Id;
            request.Date   = DateTime.Now;
            request.Status = BusinessTripRequestStatus.Unconfirmed;
            await _repositoryWrapper.BusinessTripRequests.CreateAsync(request);
            await _repositoryWrapper.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task EditAsync(User user, BusinessTripRequestDTO requestDto)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                    predicate: r => r.Id == requestDto.Id && r.UserId == requestDto.UserId
                        && r.Status == BusinessTripRequestStatus.Unconfirmed) ?? throw new NullReferenceException();
            if (requestDto.Status != BusinessTripRequestStatusDTO.Unconfirmed)
            {
                throw new InvalidOperationException();
            }
            if (!await _requestAccessService.HasAccessAsync(user, requestDto.Id))
            {
                throw new UnauthorizedAccessException();
            }
            request = _mapper.Map<BusinessTripRequestDTO, BusinessTripRequest>(requestDto);
            _repositoryWrapper.BusinessTripRequests.Update(request);
            await _repositoryWrapper.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task ConfirmAsync(User user, int id)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                                predicate: r => r.Id == id && r.Status == BusinessTripRequestStatus.Unconfirmed);
            request.Status = BusinessTripRequestStatus.Confirmed;
            _repositoryWrapper.BusinessTripRequests.Update(request);
            await _repositoryWrapper.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task CancelAsync(User user, int id)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                                predicate: r => r.Id == id && r.Status == BusinessTripRequestStatus.Confirmed);
            request.Status = BusinessTripRequestStatus.Confirmed;
            _repositoryWrapper.BusinessTripRequests.Update(request);
            await _repositoryWrapper.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(User user, int id)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                                predicate: r => r.Id == id 
                                             && (r.Status == BusinessTripRequestStatus.Unconfirmed 
                                             ||  r.Status == BusinessTripRequestStatus.UnderConsideration))
                        ?? throw new NullReferenceException();
            if (!await _requestAccessService.HasAccessAsync(user, id))
            {
                throw new UnauthorizedAccessException();
            }
            _repositoryWrapper.BusinessTripRequests.Delete(request);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
