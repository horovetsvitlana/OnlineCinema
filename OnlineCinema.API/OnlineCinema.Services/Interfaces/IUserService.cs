using OnlineCinema.DataLayer.Model;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.Services.Interfaces
{
    public interface IUserService
    {

        void CreateHash(string Password, out byte[] passwordHash);
        bool VerifyPasswordHash(string password, byte[] passwordHash);
        Task<User> FindUserAsync(string username);
        string GenerateToken(User user);
        Task<List<UserResponse>> GetUsersAsync(List<int> Ids);
    }
}
