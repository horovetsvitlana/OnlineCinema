using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
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
        [HttpPost("GetItemsByIds")]
        public async Task<List<RoleResponse>> GetItemsByIds(List<int> Ids)
        {
            return await _roleService.GetRolesAsync(Ids);

        }
    }
    //[ApiController]
    //[Route("api/[controller]")]
    //public class RoleController: Controller
    //{
    //    [Authorize]
    //    [Route("getlogin")]
    //    public IActionResult GetLogin()
    //    {
    //        return Ok($"Your Login: {User.Identity.Name}");
    //    }

    //    [Authorize(Roles = "admin")]
    //    [Route("getrole")]
    //    public IActionResult GetRole()
    //    {
    //        return Ok("Your role is administrator");
    //    }
    //}
}
