using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.RequestModels;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private CinemaDBContext _dbContext;
        private IRoleService _roleService;
        public RoleController(IRoleService roleService, CinemaDBContext dBContext)
        {
            _roleService = roleService;
            _dbContext = dBContext;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get(int Id)
        {
            Role result = await _roleService.GetRoleById(Id);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result.RoleName);
            }

        }
        [Authorize]
        [HttpGet("GetItemsByIds")]
        public async Task<List<RoleResponse>> GetItemsByIds(List<int> Ids)
        {
            return await _roleService.GetRolesAsync(Ids);

        }

        [HttpPost]
        public async Task<ActionResult<string>> AddAsync(RoleRequest roleRequest)
        {
            await _dbContext.Roles.AddAsync(new Role
            {
                Id = roleRequest.Id,
                RoleName = roleRequest.RoleName
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
