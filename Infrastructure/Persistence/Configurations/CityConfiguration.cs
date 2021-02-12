using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.Name)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.Persons)
                .WithOne(t => t.City)
                .HasForeignKey(t => t.CityId);

        }
    }
}