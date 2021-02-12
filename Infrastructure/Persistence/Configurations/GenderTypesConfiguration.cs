using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GenderTypesConfiguration : IEntityTypeConfiguration<GenderType>
    {
        public void Configure(EntityTypeBuilder<GenderType> builder)
        {
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.Persons)
                .WithOne(t => t.GenderType)
                .HasForeignKey(t => t.GenderTypeId);
        }
    }
}