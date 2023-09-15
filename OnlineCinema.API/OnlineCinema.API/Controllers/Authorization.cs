using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.DataLayer;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace OnlineCinema.API.Controllers
{

    [ApiController]
    [Route("/auth")]
    public class Authorization : ControllerBase
    {
        
        private readonly CinemaDBContext _DbContext;
        private readonly IUserService _userService;

        public Authorization(CinemaDBContext DbContext, IUserService userService)
        {
            _DbContext = DbContext;
            _userService = userService;
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            //_userService.VerifyPasswordHash(password, user)
            var identity = await GetIdentityAsync(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.Sha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            string jsonString = JsonSerializer.Serialize(response);
            return Ok(jsonString);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var person = await _DbContext.Users.FirstOrDefaultAsync(x => x.Username == username);//&& x.PasswordHash == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.RoleName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}

