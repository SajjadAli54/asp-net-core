using Microsoft.EntityFrameworkCore;

namespace MyStore.Model
{
    public class AppDbContext: DbContext
    {

        public DbSet<Client> ClientInfos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
               : base(options)
        {

        }
    }
}
