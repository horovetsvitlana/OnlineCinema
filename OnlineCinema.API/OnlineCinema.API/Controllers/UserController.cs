using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.RequestModels;
using OnlineCinema.Shared.ResponseModels;
using System.Text;

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
            var user = await _userService.FindUserAsync(username);

            _userService.CreateHash(password, out byte[] passwordHash);
            byte[] passHash = Encoding.ASCII.GetBytes(user.Password);
            if (passHash != passwordHash)
            {
                return BadRequest();
            }
            if (user.Username != username)
            {
                return BadRequest("User not found");
            }


            string token = _userService.GenerateToken(user);
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
                Birthday = user.Birthday,
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

