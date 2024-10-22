﻿// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

Console.WriteLine("Hello, World!");

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string University { get; set; }


    public ICollection<Certificate> Certificates { get; set; }
}

public class Certificate
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string Name { get; set; }

    public string Institution { get; set; }

    
    public Student Student { get; set; }
}

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasMany(s => s.Certificates)
               .WithOne(c => c.Student)
               .HasForeignKey(c => c.StudentId);
    }
}


public class EducationDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
   
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EducationDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Data Seeding
        // data seeding'de Id leri yazmak zorundayız.
        modelBuilder.Entity<Student>()
                    .HasData(
            new Student() { Id=1,Name="Halil",University="Duzce University" },
            new Student() { Id=2,Name="Ela",University="Duzce University" }
            );

        modelBuilder.Entity<Certificate>()
                    .HasData(
            new Certificate() { Id=1,Name=".Net Core",Institution="Udemy", StudentId = 1},
            new Certificate() { Id=2,Name="Docker",Institution="Udemy", StudentId = 1 },
            new Certificate() { Id=3,Name="Angular",Institution="Turkcell", StudentId = 2 },
            new Certificate() { Id=4,Name="Linux",Institution="Turkcell", StudentId = 2 },
            new Certificate() { Id=5,Name="Web Api",Institution="Turkcell", StudentId = 2 }
            );
    }
}