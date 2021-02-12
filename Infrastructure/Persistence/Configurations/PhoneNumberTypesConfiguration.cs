using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PhoneNumberTypesConfiguration : IEntityTypeConfiguration<PhoneNumberType>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberType> builder)
        {
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.PhoneNumbers)
                .WithOne(t => t.PhoneNumberType)
                .HasForeignKey(t => t.PhoneNumberTypeId);
        }
    }
}