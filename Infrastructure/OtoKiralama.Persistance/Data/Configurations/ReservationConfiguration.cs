using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Data.Configurations
{
    public class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.IsPaid).HasDefaultValue(0);
            builder.Property(r => r.IsCanceled).HasDefaultValue(0);
        }
    }
}
