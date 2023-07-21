using AutoMapper;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Repositories.Interfaces;
using OnlineCinema.Shared.ResponseModels;
using OnlineCinema.DataLayer.Model;

namespace OnlineCinema.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<List<UserResponse>> GetUsersAsync(List<int> Ids)
        {
            List<User> users = await _userRepository.GetUserWithRoleAsync(Ids);
            List<UserResponse> result = new List<UserResponse>();
            foreach(User user in users)
            {
                UserResponse response = new UserResponse()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    CreatedDate = user.CreatedDate,
                    LastModifiedDate = user.LastModifiedDate,
                    DateOfBirth = user.DateOfBirth,
                    MonthOfBirth = user.MonthOfBirth,
                    YearOfBirth = user.YearOfBirth,
                    IsDeleted = user.IsDeleted,
                    RoleId = user.RoleId,
                    Role = user.Role.RoleName
                };
                result.Add(response);
            }
            return result;
        }
    }
}
