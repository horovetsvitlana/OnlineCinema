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
        private IUserService _userService;
        private CinemaDBContext _dbContext;
        private IConfiguration _configuration;
        public UserController(CinemaDBContext dbContext, IUserService userService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userService = userService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("GetItemsByIds/{Id}")]
        public async Task<ActionResult<UserResponse>> Get(int Id)
        {
            var result = (await _userService.GetUsersAsync(new List<int> { Id})).FirstOrDefault();
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("getToken")]
        public async Task<ActionResult<string>> CreationOfTokenAsync(string username, string password)
        {
            _userService.FindUserAsync(username);
            var user = new User();

            _userService.CreateHash(password, out byte[] passwordHash);
            if (passwordHash != user.PasswordHash)
            {
                return BadRequest();
            }
            if (user.Username != username)
            {
                return BadRequest("User not found");
            }

            if (!_userService.VerifyPasswordHash(password, user.PasswordHash))
            {
                return BadRequest("something went wrong");
            };

            string token = _userService.GenerateToken();
            return Ok(token);


        }
        [HttpPost]
        public async Task<ActionResult<string>> AddAsync(UserRequest user, IUserService _userService)
        {
            _userService.CreateHash(user.Password, out byte[] passwordHash);
            await _dbContext.Users.AddAsync(new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = passwordHash,
                DateOfBirth = user.DateOfBirth,
                MonthOfBirth = user.MonthOfBirth,
                YearOfBirth = user.YearOfBirth,
                CreatedDate = user.CreatedDate,
                LastModifiedDate = user.LastModifiedDate,
                IsDeleted = user.IsDeleted,
                RoleId = user.RoleId

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

