using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Ignore(t => t.DomainEvents);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.OwnsOne(m => m.Name, a =>
            {
                a.Property(p => p.FirstName).HasMaxLength(200)
                    .HasColumnName("FirstName")
                    .IsRequired();
                a.Property(p => p.LastName).HasMaxLength(200)
                    .HasColumnName("LastName")
                    .IsRequired();
            }).Navigation(t => t.Name).IsRequired();

            builder.OwnsOne(m => m.PersonalNumber, a =>
            {
                a.HasIndex(t => t.Value)
                    .IsUnique();

                a.Property(p => p.Value).HasMaxLength(200)
                    .HasColumnName("PersonalNumber")
                    .IsRequired();

            }).Navigation(t => t.PersonalNumber).IsRequired();

            builder.OwnsOne(m => m.BirthDate, a =>
            {
                a.Property(p => p.Value)
                    .HasColumnName("BirthDate")
                    .IsRequired();

            }).Navigation(t => t.BirthDate).IsRequired();

            builder.HasOne(e => e.GenderType)
                .WithMany(e => e.Persons)
                .HasForeignKey(e => e.GenderTypeId);

            builder.HasOne(e => e.City)
                .WithMany(e => e.Persons)
                .HasForeignKey(e => e.CityId);

            builder.HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);

            builder.HasMany(e => e.DirectRelatedPersons)
                .WithOne(e => e.PersonFor)
                .HasForeignKey(e => e.PersonForId);

            builder.HasMany(e => e.IndirectRelatedPersons)
                .WithOne(e => e.PersonTo)
                .HasForeignKey(e => e.PersonToId);
        }
    }
}