using Microsoft.EntityFrameworkCore;

namespace ApiCURD__practice.Model
{
    public class AppDbContext:DbContext
    {
      
       public AppDbContext(DbContextOptions options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
    }
}
