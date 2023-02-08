using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Pais> provincia { get; set; }
        public DbSet<Provincia> provincias { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
    }
}
