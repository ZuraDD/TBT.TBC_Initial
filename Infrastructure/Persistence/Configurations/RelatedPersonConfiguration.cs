using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.Ignore(t => t.DomainEvents);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.HasIndex(u => new { u.PersonForId, u.PersonToId }).IsUnique();

            builder.HasOne(t => t.RelationType)
                .WithMany(t => t.RelatedPersons)
                .HasForeignKey(t => t.RelationTypeId);

            builder.HasOne(t => t.PersonFor)
                .WithMany(t => t.DirectRelatedPersons)
                .HasForeignKey(t => t.PersonForId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.PersonTo)
                .WithMany(t => t.IndirectRelatedPersons)
                .HasForeignKey(t => t.PersonToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}