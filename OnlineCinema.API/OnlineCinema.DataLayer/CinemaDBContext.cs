using Microsoft.EntityFrameworkCore;
using OnlineCinema.DataLayer.Model;

namespace OnlineCinema.DataLayer
{
    public class CinemaDBContext : DbContext
    {
        private readonly string connectionString = string.Empty;
        public CinemaDBContext() { }
        public CinemaDBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CinemaDBContext(DbContextOptions<CinemaDBContext> options) : base(options)
        {
            var getprovider = options.Extensions.FirstOrDefault(p => p is
            Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension);
            if (getprovider != null)
            {
                connectionString = ((Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension)getprovider).ConnectionString;
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}