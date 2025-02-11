using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtoKiralama.Domain.Entities;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(m => m.Cars)
               .WithOne(c => c.Model)
               .HasForeignKey(c => c.ModelId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
