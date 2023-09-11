// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello, World!");

ECommerceDbContext db = new();
#region
//Product p1 = new() 
//{
//    Name = "Saat", 
//    Price = 1000, 
//    Quantity = "2", 
//    Categories = 
//    { 
//        new ProductCategory() { Category = new Category() {Name = "Elektronik" } },
//        new ProductCategory() { Category = new Category() {Name = "Eğlence"}},
//        new ProductCategory() { Category = new Category() {Name = "Spor"}}
//    } 
//};

//Product p2 = new() 
//{ 
//    Name = "Bardak", Price = 50, Quantity = "2", Categories = 
//    { 
//        new() {Category = new Category(){Name ="Ev Eşyaları" } },  
//    } 
//};

//Product p3 = new()
//{
//    Name = "Telefon",
//    Price = 50,
//    Quantity = "81",
//    Categories =
//    {
//        new() {CategoryId=1},
//        new() {CategoryId=2}
//    }
//};

//Product p4 = new()
//{
//    Name = "paten",
//    Price = 3500,
//    Quantity = "72",
//    Categories =
//    { 
//        new() {CategoryId=1 },
//        new() {CategoryId=3},
//        new() {Category=new Category(){Name="Paten"} }
//    }
//};



//db.Products.AddRange(p1, p2, p3, p4);
//db.SaveChanges();

//var p = db.Products.Include(p => p.Categories).FirstOrDefault(p => p.Id == 8);

//var cat = db.Categories.ToList();

//foreach (var c in p.Categories)
//{
//    Console.WriteLine(c.CategoryId + " " + c.Category.Name);
//}
#endregion

#region
//Supplier s1= new()
//{
//    Name = "GBilişim",
//    SQuantity = 5,
//    Products = { 
//        new ProductSupplier(){ProductId = 5},
//        new ProductSupplier(){ProductId = 7}
//    }
//};

//Supplier s2 = new()
//{
//    Name = "Skate",
//    SQuantity = 15,
//    Products = {
//        new ProductSupplier(){ProductId = 6},
//        new ProductSupplier(){ProductId = 8}
//    }
//};

//Supplier s3 = new()
//{
//    Name = "Ncam",
//    SQuantity = 121,
//    Products = {
//        new ProductSupplier(){ProductId = 6}
//    }
//};

//db.Suppliers.AddRange(s1 , s2 , s3);
//db.SaveChanges();

#endregion


public class Product
{
    public Product()
    {
        Categories = new List<ProductCategory>();
        Suppliers = new List<ProductSupplier>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public float Price { get; set; }

    public string Quantity { get; set; }


    public ICollection<ProductCategory> Categories { get; set; }

    public ICollection<ProductSupplier> Suppliers { get; set; }
}


public class Category
{
    public Category()
    {
        Products = new List<ProductCategory>();
    }

    public int Id { get; set; }

    public string Name { get; set; }


    public ICollection<ProductCategory> Products { get; set; }
}

public class Supplier
{
    public Supplier()
    {
        Products = new List<ProductSupplier>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int SQuantity { get; set; }


    public ICollection<ProductSupplier> Products { get; set; }
}

//cross table
public class ProductCategory
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }


    public Product Product { get; set; }
    public Category Category { get; set; }
}
//cross table
public class ProductSupplier
{
    public int ProductId { get; set; }

    public int SupplierId { get; set; }


    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
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
        modelBuilder.Entity<ProductCategory>()
                    .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        modelBuilder.Entity<ProductCategory>()
                    .HasOne(pc => pc.Product)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductCategory>()
                    .HasOne(pc => pc.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(pc => pc.CategoryId);



        modelBuilder.Entity<ProductSupplier>()
                    .HasKey(pc => new {pc.ProductId, pc.SupplierId});

        modelBuilder.Entity<ProductSupplier>()
                    .HasOne(pc => pc.Supplier)
                    .WithMany(s=>s.Products)
                    .HasForeignKey(pc=>pc.SupplierId);
        
        modelBuilder.Entity<ProductSupplier>()
                    .HasOne(pc => pc.Product)
                    .WithMany(p=>p.Suppliers)
                    .HasForeignKey(pc => pc.ProductId);
          
    }
}