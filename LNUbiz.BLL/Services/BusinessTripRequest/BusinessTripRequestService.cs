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
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IBusinessTripRequestAccessService _requestAccessService;
        private readonly IMapper _mapper;

        public BusinessTripRequestService(IRepositoryWrapper repositoryWrapper, IBusinessTripRequestAccessService requestAccessService,
        IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _requestAccessService = requestAccessService;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<BusinessTripRequestDTO> GetByIdAsync(User user, int id)
        {
            var request = await _repositoryWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                    predicate: a => a.Id == id,
                    include: source => source
                        .Include(a => a.User));
            return await _requestAccessService.HasAccessAsync(user, id) ? _mapper.Map<BusinessTripRequest, BusinessTripRequestDTO>(request)
                : throw new UnauthorizedAccessException();
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<BusinessTripRequestDTO>> GetAllAsync(User user)
        {
            //TODO: implement
            return _mapper.Map<IEnumerable<BusinessTripRequest>, IEnumerable<BusinessTripRequestDTO>>(new List<BusinessTripRequest>(){});
        }

        ///<inheritdoc/>
        public async Task CreateAsync(User user, BusinessTripRequestDTO requestDto)
        {
            //TODO: implement
        }

        ///<inheritdoc/>
        public async Task EditAsync(User user, BusinessTripRequestDTO requestDto)
        {
            //TODO: implement
        }

        ///<inheritdoc/>
        public async Task ConfirmAsync(User user, int id)
        {
            //TODO: implement
        }

        ///<inheritdoc/>
        public async Task CancelAsync(User user, int id)
        {
            //TODO: implement
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(User user, int id)
        {
            //TODO: implement
        }


        public async Task<BusinessTripRequestDTO> GetEditFormByIdAsync(User user, int id)
        {
            //TODO: implement

            return _mapper.Map<BusinessTripRequest, BusinessTripRequestDTO>(new BusinessTripRequest());
        }
    }
}
