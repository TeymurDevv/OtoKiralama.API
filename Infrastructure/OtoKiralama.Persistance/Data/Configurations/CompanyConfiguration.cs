using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.HasMany(b => b.Cars)
               .WithOne(m => m.Company)
               .HasForeignKey(m => m.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.AppUsers)
               .WithOne(m => m.Company)
               .HasForeignKey(m => m.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
