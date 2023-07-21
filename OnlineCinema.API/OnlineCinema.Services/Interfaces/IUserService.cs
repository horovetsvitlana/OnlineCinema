using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetUsersAsync(List<int> Ids);
    }
}
