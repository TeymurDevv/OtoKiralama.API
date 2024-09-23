using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Persistance.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Gear> Gears { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
