using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Persistance.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Gear> Gears { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
