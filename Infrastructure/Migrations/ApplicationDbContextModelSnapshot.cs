﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("City");
                });

            modelBuilder.Entity("Domain.Entities.GenderType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("GenderType");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GenderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("GenderTypeId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Domain.Entities.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PhoneNumberTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PhoneNumberTypeId");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("Domain.Entities.PhoneNumberType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PhoneNumberType");
                });

            modelBuilder.Entity("Domain.Entities.Relation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonForId")
                        .HasColumnType("int");

                    b.Property<int>("PersonToId")
                        .HasColumnType("int");

                    b.Property<int>("RelationTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonToId");

                    b.HasIndex("RelationTypeId");

                    b.HasIndex("PersonForId", "PersonToId")
                        .IsUnique();

                    b.ToTable("Relation");
                });

            modelBuilder.Entity("Domain.Entities.RelationType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("RelationType");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.HasOne("Domain.Entities.City", "City")
                        .WithMany("Persons")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.GenderType", "GenderType")
                        .WithMany("Persons")
                        .HasForeignKey("GenderTypeId");

                    b.OwnsOne("Domain.ValueObjects.BirthDateVO", "BirthDate", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2")
                                .HasColumnName("BirthDate");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("Domain.ValueObjects.PersonNameVO", "Name", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("LastName");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("Domain.ValueObjects.PersonalNumberVO", "PersonalNumber", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("PersonalNumber");

                            b1.HasKey("PersonId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[PersonalNumber] IS NOT NULL");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("BirthDate")
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("GenderType");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("PersonalNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.PhoneNumber", b =>
                {
                    b.HasOne("Domain.Entities.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PhoneNumberType", "PhoneNumberType")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PhoneNumberTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PhoneNumberType");
                });

            modelBuilder.Entity("Domain.Entities.Relation", b =>
                {
                    b.HasOne("Domain.Entities.Person", "PersonFor")
                        .WithMany("DirectRelatedPersons")
                        .HasForeignKey("PersonForId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Person", "PersonTo")
                        .WithMany("IndirectRelatedPersons")
                        .HasForeignKey("PersonToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RelationType", "RelationType")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("RelationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonFor");

                    b.Navigation("PersonTo");

                    b.Navigation("RelationType");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("Domain.Entities.GenderType", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Navigation("DirectRelatedPersons");

                    b.Navigation("IndirectRelatedPersons");

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("Domain.Entities.PhoneNumberType", b =>
                {
                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("Domain.Entities.RelationType", b =>
                {
                    b.Navigation("RelatedPersons");
                });
#pragma warning restore 612, 618
        }
    }
}
