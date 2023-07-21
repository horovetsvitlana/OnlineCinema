using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repository.Interfaces;

namespace OnlineCinema.Repositories.Interfaces
{
    public interface IUserRepository : IBaseSqlRepository< User, CinemaDBContext>
    {
        Task<List<User>> GetUserWithRoleAsync(List<int> Ids);
    }
}
