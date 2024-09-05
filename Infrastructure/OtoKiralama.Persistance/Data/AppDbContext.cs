using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
