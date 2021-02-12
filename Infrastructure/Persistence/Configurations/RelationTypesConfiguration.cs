using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RelationTypesConfiguration : IEntityTypeConfiguration<RelationType>
    {
        public void Configure(EntityTypeBuilder<RelationType> builder)
        {
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.RelatedPersons)
                .WithOne(t => t.RelationType)
                .HasForeignKey(t => t.RelationTypeId);
        }
    }
}