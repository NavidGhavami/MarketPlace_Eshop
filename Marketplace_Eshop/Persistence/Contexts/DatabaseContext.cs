using Application.Interfaces.Contexts;
using Domian.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext, IDataBaseContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

    }
}
