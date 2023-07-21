using OnlineCinema.DataLayer.Model;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetRolesAsync(List<int> Ids);
        Task<Role> GetRoleById(int Id);
    }
}
