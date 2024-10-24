using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Data.Configurations
{
    public class CarConfiguration:IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(1);
        }
    }
}
