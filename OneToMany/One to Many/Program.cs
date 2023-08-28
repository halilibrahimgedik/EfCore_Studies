// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


// her ürünün 1 kategorisi var olarak kabul edelim

public class Product //dependent
{
    public int Id { get; set; }

    public string ProductName { get; set; }


    public Category Category { get; set; }
}

public class Category
{

    public int Id { get; set; }

    public string CategoryName { get; set; }


    public ICollection<Product> Products { get; set; }
}


public class EcommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=GedikPC\MSSQLSERVERR;Database=Ecommerce;Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products);
    }
}