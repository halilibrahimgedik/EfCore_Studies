// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

SchoolContext context = new SchoolContext();

#region Adding Data - One To One Relationship
// Principal Entity Üzerinden Dependent Entity Verisi Ekleme
//Student student1 = new Student()
//{
//    Name="Taner",
//    Adress = {City="Ankara"}
//};

//context.Add(student1);
//context.SaveChanges();

//public class Student
//{
//    public Student()
//    {
//        Adress = new Adress();
//    }
//    public int StudentId { get; set; }

//    public string Name { get; set; }


//    public Adress Adress { get; set; }
//}

//public class Adress
//{
//    public int AdressId { get; set; }

//    public string City { get; set; }

//    public Student Student { get; set; }
//}

//public class SchoolContext : DbContext
//{
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server=GEDIKPC\SQLEXPRESS;Database=DataCrud;Integrated Security=true");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Student>()
//                    .HasOne(s => s.Adress)
//                    .WithOne(a => a.Student)
//                    .HasForeignKey<Adress>(a => a.AdressId);
//        modelBuilder.Entity<Adress>()
//                    .HasKey(a => a.AdressId);
//    }
//}
#endregion

#region Adding Data - One To Many RelationShip
// 1-) Principal tablo üzerinden veri ekleme
//Blog technology = new Blog()
//{
//    BlogName = "Technology",
//    Posts = {
//        new Post() { PostHead="Yazılım için bilgisayar tavsiyeleri"} , 
//        new Post() { PostHead="Programlama dilleri"} 
//    }
//};

//context.Blogs.Add(technology);
//context.SaveChanges();

// 2-) dependent tabo üzerinden veri ekleme - Pek tavsiye edilmez 
//var query = context.Blogs.Find(1);
//Post post = new Post()
//{
//    Blog = query,
//    PostHead="Yapay Zeka"
//};

//context.Posts.Add(post);
//context.SaveChanges();

// 3-) Not: diğer bir yöntem ise Classlar arasındaki ilişkiyi Fluent API ile değilde Default Conventions ile tanımlasaydık Dependent classdaki Foreign Key alanı ile veri ekleyebilirdik

//public class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int BlogId { get; set; }

//    public string BlogName { get; set; }


//    public ICollection<Post> Posts { get; set; }
//}

//public class Post
//{
//    public int PostId { get; set; }

//    public string PostHead { get; set; }


//    public Blog Blog { get; set; }
//}

//public class SchoolContext : DbContext
//{
//    public DbSet<Blog> Blogs { get; set; }

//    public DbSet<Post> Posts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(@"Server=GEDIKPC\SQLEXPRESS;Database=DataCrud;Integrated Security=true");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Blog>()
//                    .HasMany(b => b.Posts)
//                    .WithOne(p => p.Blog);
//        // no need for foreign key, Ef core will get it
//    }
//}
#endregion

#region Adding Data - Many To Many 

// 1-) Fluent Apı -  veri ekle

Product product = new()
{   
    ProductName="Bilgisayar",
    Categories = {
        new(){CategoryId=2, Category=new(){CategoryName="Beyaz Eşya"}},
    }

};
context.Add(product);
context.SaveChanges();

// 2-) Data Conventions  ile de veri eklenebilir fakat bunun için cross table a ihtiyacımız yok unutmayın

public class Product
{
    public Product()
    {
        Categories = new HashSet<ProductCategory>();
    }

    public int Id { get; set; }

    public string ProductName { get; set; }


    public ICollection<ProductCategory> Categories { get; set; }
}


public class ProductCategory
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }


    public Product Product { get; set; }

    public Category Category { get; set; }

}


public class Category
{
    public Category()
    {
        Products= new HashSet<ProductCategory>();
    }

    public int Id { get; set; }

    public string CategoryName { get; set; }


    public ICollection<ProductCategory> Products { get; set; }
}


public class SchoolContext : DbContext
{
    //public Dbset<Product> Products { get; set; }

    //public Dbset<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=GEDIKPC\SQLEXPRESS;Database=DataCrud;Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new {pc.CategoryId,pc.ProductId });

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc=>pc.Product)
            .WithMany(p=>p.Categories)
            .HasForeignKey(pc=>pc.CategoryId);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc=>pc.Category)
            .WithMany(p=>p.Products)
            .HasForeignKey(pc=>pc.ProductId);
    }
}
#endregion
