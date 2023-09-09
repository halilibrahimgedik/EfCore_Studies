﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataSeeding.Migrations
{
    [DbContext(typeof(EducationDbContext))]
    [Migration("20230909102301_mig2")]
    partial class mig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("certificates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Institution = "Udemy",
                            Name = ".Net Core",
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            Institution = "Udemy",
                            Name = "Docker",
                            StudentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Institution = "Turkcell",
                            Name = "Angular",
                            StudentId = 2
                        },
                        new
                        {
                            Id = 4,
                            Institution = "Turkcell",
                            Name = "Angular",
                            StudentId = 2
                        },
                        new
                        {
                            Id = 5,
                            Institution = "Turkcell",
                            Name = "Angular",
                            StudentId = 2
                        });
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Halil",
                            University = "Duzce University"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ela",
                            University = "Duzce University"
                        });
                });

            modelBuilder.Entity("Certificate", b =>
                {
                    b.HasOne("Student", "Student")
                        .WithMany("Certificates")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Navigation("Certificates");
                });
#pragma warning restore 612, 618
        }
    }
}
