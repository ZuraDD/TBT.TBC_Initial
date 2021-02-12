using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.Ignore(t => t.DomainEvents);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.HasIndex(x => x.Value).IsUnique();

            builder.Property(t => t.Value)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.PhoneNumberTypeId)
                .IsRequired();

            builder.HasOne(t => t.PhoneNumberType)
                .WithMany(t => t.PhoneNumbers)
                .HasForeignKey(t => t.PhoneNumberTypeId);
        }
    }
}