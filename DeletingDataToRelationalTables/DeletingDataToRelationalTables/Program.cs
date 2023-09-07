// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

Context context = new();

#region Hazır Test Verileri
//Student s1 = new Student()
//{
//    Name = "Halil",
//    Adress = new Adress() { City = "İstanbul" }

//};

//Student s2 = new Student()
//{
//    Name = "Ela",
//    Adress = new Adress() { City = "İstanbul" }

//};

//context.Students.AddRange(s1,s2);

//Blog blog = new Blog()
//{
//    BlogName = "Teknoloji",
//    Posts = new List<Post>()
//    {
//        new Post(){PostHead="Bir Yazılımcının hikayesi"},
//        new Post(){PostHead="Yazılım Yol Haritası"}
//    }
//};
//Blog blog2 = new Blog()
//{
//    BlogName = "Yapay Zeka",
//    Posts = new List<Post>()
//    {
//        new Post(){PostHead="Yapay Zeka nedir ?"},
//        new Post(){PostHead="Yapay Zeka İnsanlığı yok mu edecek ?"}
//    }
//};
//Blog blog3 = new Blog()
//{
//    BlogName = "Gündem",
//    Posts = new List<Post>()
//    {
//        new Post(){PostHead="Yeni dönem Heyecanı"},
//        new Post(){PostHead="Mastercheff kim elendi ?"}
//    }
//};
//context.Blogs.AddRange(blog,blog2,blog3);

//context.SaveChanges();

#endregion

#region deleting Data - One To One ilişkili tablolarda veri silme

//  2 id sine sahip öğrencinin adresini silelim 

//var student = context.Students.Include(s => s.Adress).FirstOrDefault(s => s.StudentId == 2);

//if (student != null)
//{
//    context.Remove(student.Adress);
//}
//context.SaveChanges();

public class Student
{
    public Student()
    {
        Adress = new Adress();
    }
    public int StudentId { get; set; }

    public string Name { get; set; }


    public Adress Adress { get; set; }
}

public class Adress
{
    public int AdressId { get; set; }

    public string City { get; set; }

    public Student Student { get; set; }
}

#endregion

#region deleting Data - One To Many RelationShip
// 1-) dependent tablodan veri silme 
//Blog? b = context.Blogs.Include(x => x.Posts).FirstOrDefault(x => x.BlogId == 3);

//if (b != null)
//{
//    /*context.Blogs.Remove(b);*/    //  bu tüm bloğu ve ona bağlı olan postları siler

//    var post = b.Posts.FirstOrDefault(x => x.PostId == 6);
//    if (post != null)
//    {
//        b.Posts.Remove(post);
//    }
//}

//context.SaveChanges();

public class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int BlogId { get; set; }

    public string BlogName { get; set; }


    public ICollection<Post> Posts { get; set; }
}

public class Post
{
    public Post()
    {
        Blog = new Blog();
    }
    public int PostId { get; set; }

    public string PostHead { get; set; }


    public Blog Blog { get; set; }
}

#endregion

#region deleting Data - Many To Many - Always Using Cascade Delete


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
        Products = new HashSet<ProductCategory>();
    }

    public int Id { get; set; }

    public string CategoryName { get; set; }


    public ICollection<ProductCategory> Products { get; set; }
}

#endregion

public class Context : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Adress> Adresses { get; set; }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=GEDIKPC\SQLEXPRESS;Database=DataCrud;Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
                    .HasOne(s => s.Adress)
                    .WithOne(a => a.Student)
                    .HasForeignKey<Adress>(a => a.AdressId);
        modelBuilder.Entity<Adress>()
                    .HasKey(a => a.AdressId);



        modelBuilder.Entity<Blog>()
                    .HasMany(b => b.Posts)
                    .WithOne(p => p.Blog);
        // no need for foreign key, Ef core will get it



        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.CategoryId, pc.ProductId });

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.Categories)
            .HasForeignKey(pc => pc.CategoryId);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(pc => pc.ProductId);
    }
}

