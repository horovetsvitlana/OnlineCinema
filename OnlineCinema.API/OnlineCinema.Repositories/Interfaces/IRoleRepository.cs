using OnlineCinema.DataLayer;
using OnlineCinema.DataLayer.Model;
using OnlineCinema.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Repositories.Interfaces
{
    public interface IRoleRepository: IBaseSqlRepository<Role, CinemaDBContext>
    {
    }
}
