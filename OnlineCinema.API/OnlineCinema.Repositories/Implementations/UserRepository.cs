using Microsoft.EntityFrameworkCore;
using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repositories.Interfaces;
using OnlineCinema.Repository.Implementation;
using System.Linq;

namespace OnlineCinema.Repositories.Implementations
{
    public class UserRepository : BaseSQLRepository<User, CinemaDBContext>, IUserRepository
    {
        private CinemaDBContext _dbContext;
        public UserRepository(CinemaDBContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> GetUserWithRoleAsync(List<int> Ids)
        {
            var query = _dbContext.Users.Where(x => Ids.Contains(x.Id)).Include(x => x.Role);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
