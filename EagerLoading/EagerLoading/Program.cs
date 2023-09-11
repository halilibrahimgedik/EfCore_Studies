// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ECommerceDbContext db = new();

#region ThenInclude()
var categories = db.Categories.Include(c => c.Products)
                              .ThenInclude(p => p.Suppliers)
                              .ToList();

#endregion

foreach (var item in categories)
{
    Console.WriteLine(item.Name);
    Console.WriteLine("Products adet: " + item.Products.Count);
}

public class Product
{
    public Product()
    {
        Categories = new List<Category>();
        Suppliers = new List<Supplier>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public float Price { get; set; }

    public string Quantity { get; set; }


    public ICollection<Category> Categories { get; set; }

    public ICollection<Supplier> Suppliers { get; set; }
}


public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }

    public int Id { get; set; }

    public string Name { get; set; }


    public ICollection<Product> Products { get; set; }
}

public class Supplier
{
    public Supplier()
    {
        Products = new List<Product>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int SQuantity { get; set; }


    public ICollection<Product> Products { get; set; }
}


public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }
   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EagerLoadingDb;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          
    }
}