using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Persistance.Data.Configurations
{
    public class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(s=>s.IsSmsSubscribed).HasDefaultValue(false);
            builder.Property(s => s.IsEmailSubscribed).HasDefaultValue(false);
        }
    }
}
