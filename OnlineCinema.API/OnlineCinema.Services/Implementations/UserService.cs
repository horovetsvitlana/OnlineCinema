using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repositories.Interfaces;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OnlineCinema.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly CinemaDBContext _dbContext;
        public UserService(IMapper mapper, IUserRepository userRepository, CinemaDBContext dbContext, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public void CreateHash(string Password, out byte[] passwordHash)
        {
            using (var sha256 = SHA256.Create())
            {
                passwordHash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }
        public void FindUserAsync(string username)
        {
            var query = _dbContext.Users.Where(u => username.Contains(u.Username));
        }
        
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   expires: DateTime.UtcNow.AddDays(1),
                                   signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public async Task<List<UserResponse>> GetUsersAsync(List<int> Ids)
        {
            List<User> users = await _userRepository.GetUserWithRoleAsync(Ids);
            List<UserResponse> result = new List<UserResponse>();
            foreach (User user in users)
            {
                UserResponse response = new UserResponse()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedDate = user.CreatedDate,
                    LastModifiedDate = user.LastModifiedDate,
                    DateOfBirth = user.DateOfBirth,
                    MonthOfBirth = user.MonthOfBirth,
                    YearOfBirth = user.YearOfBirth,
                    IsDeleted = user.IsDeleted,
                    RoleId = user.RoleId
                    //Role = user.Role.RoleName
                };
                result.Add(response);
            }
            return result;
        }
    }
}
