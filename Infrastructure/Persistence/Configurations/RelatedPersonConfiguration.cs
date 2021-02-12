﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
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
                .HasForeignKey(t => t.PersonForId);

            builder.HasOne(t => t.PersonTo)
                .WithMany(t => t.IndirectRelatedPersons)
                .HasForeignKey(t => t.PersonToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}