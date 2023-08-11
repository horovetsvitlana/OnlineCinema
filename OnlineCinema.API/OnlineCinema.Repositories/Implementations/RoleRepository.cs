using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repositories.Interfaces;
using OnlineCinema.Repository.Implementation;

namespace OnlineCinema.Repositories.Implementations
{
    public class RoleRepository: BaseSQLRepository<Role, CinemaDBContext>, IRoleRepository
    {
        public RoleRepository( CinemaDBContext dBContext ) : base( dBContext ) { }
    }
}
