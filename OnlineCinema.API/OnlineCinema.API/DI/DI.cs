using OnlineCinema.Services.Implementations;
using OnlineCinema.Services.Interfaces;
using OnlineCinema.Shared.AutoMapper;
using OnlineCinema.Repositories.Implementations;
using OnlineCinema.Repositories.Interfaces;

namespace OnlineCinema.API.DI
{
    public static class DI
    {
        public static IServiceCollection AddDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddAutoMapper(conf =>
            {
                conf.AddProfile(typeof( AutoMapperProfile));
            });

            return services;
        }
    }
}
