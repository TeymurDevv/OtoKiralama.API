using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(b => b.Models)
               .WithOne(m => m.Brand)
               .HasForeignKey(m => m.BrandId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
