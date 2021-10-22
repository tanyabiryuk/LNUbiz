using AutoMapper;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.Admin;
using LNUbiz.BLL.Interfaces.Admin;
using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;

namespace LNUbiz.BLL.Services.Admin
{
    public class AdminTypeService : IAdminTypeService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public AdminTypeService(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        public async Task<AdminTypeDTO> GetAdminTypeByNameAsync(string name)
        {
            var adminType = await _repoWrapper.AdminType.GetFirstOrDefaultAsync(i => i.AdminTypeName == name);
            return adminType != null
                ? _mapper.Map<AdminType, AdminTypeDTO>(adminType)
                : await this.CreateByNameAsync(name);
        }

        public async Task<AdminTypeDTO> GetAdminTypeByIdAsync(int adminTypeId)
        {
            var adminType = await _repoWrapper.AdminType.GetFirstOrDefaultAsync(i => i.ID == adminTypeId);
            return _mapper.Map<AdminType, AdminTypeDTO>(adminType);
        }

        public async Task<AdminTypeDTO> CreateByNameAsync(string adminTypeName)
        {
            return await this.CreateAsync(new AdminTypeDTO() { AdminTypeName = adminTypeName });
        }

        public async Task<AdminTypeDTO> CreateAsync(AdminTypeDTO adminTypeDto)
        {
            var newAdminType = _mapper.Map<AdminTypeDTO, AdminType>(adminTypeDto);
            await _repoWrapper.AdminType.CreateAsync(newAdminType);
            await _repoWrapper.SaveAsync();

            return _mapper.Map<AdminType, AdminTypeDTO>(newAdminType);
        }
    }
}
