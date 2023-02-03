using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Pais> paises { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
    }
}
