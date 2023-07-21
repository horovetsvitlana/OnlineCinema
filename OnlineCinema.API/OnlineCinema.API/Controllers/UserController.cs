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
    public class UserController : ControllerBase
    {
        private CinemaDBContext _dbContext;
        private IUserService _userService;
        public UserController(CinemaDBContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;

        }

        [Authorize]
        [HttpGet("GetItemsByIds/{Id}")]
        public async Task<ActionResult<UserResponse>> GetItemsByIds(int Id)
        {
            var result = (await _userService.GetUsersAsync(new List<int> { Id })).FirstOrDefault();
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddAsync(UserRequest user)
        {
            await _dbContext.Users.AddAsync(new User
            {
                Login = user.Login,
                //RoleId = user.,
                DateOfBirth = user.DateOfBirth,
                MonthOfBirth = user.MonthOfBirth,
                YearOfBirth = user.YearOfBirth,
                CreatedDate = user.CreatedDate,
                LastModifiedDate = user.LastModifiedDate,
                IsDeleted = user.IsDeleted
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("GetItemsByIds")]
        public async Task<List<UserResponse>> GetItemsByIds(List<int> Ids)
        {
            return await _userService.GetUsersAsync(Ids);
        }

    }


}

