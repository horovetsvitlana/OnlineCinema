using AutoMapper;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Shared.ResponseModels;

namespace OnlineCinema.Shared.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<Role, RoleResponse>().ReverseMap();
        }
    }
}
