using AutoMapper;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repositories.Interfaces;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.Services.Implementations
{
    public class RoleService: IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<List<RoleResponse>> GetRolesAsync(List<int> Ids)
        {
            List<Role> roles = await _roleRepository.GetAllByConditionAsync( x => Ids.Contains(x.Id));
            return _mapper.Map<List<RoleResponse>>(roles);
        }
        public async Task<Role> GetRoleById(int Id)
        {
            var role = (await _roleRepository.GetAllByConditionAsync(x => x.Id == Id)).FirstOrDefault();
            return role;
        }
    }
}
