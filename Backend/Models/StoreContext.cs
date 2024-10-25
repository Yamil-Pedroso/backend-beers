using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class StoreContext : DbContext // DbContext es una clase de EntityFramework
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options)
        { 

        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
