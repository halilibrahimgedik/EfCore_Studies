// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

#region /* FLUENT API /  One To One Relationship , biraz garip bir örnek ama aklıma anlık bu geldi :) */

//public class Husband
//{
//    public int HusbandId { get; set; }

//    public string HusbandName { get; set; }

//    public string Age { get; set; }


//    public Wife Wife { get; set; } // navigation property
//}

//public class Wife
//{
//    public int WifeId { get; set; }

//    public string WifeName { get; set; }

//    public string Age { get; set; }


//    public Husband Husband { get; set; } // navigation property
//}



//public class MarriageContext : DbContext
//{
//    public DbSet<Husband> Husbands { get; set; }

//    public DbSet<Wife> Wives { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=GEDIKPC\\SQLEXPRESS;Database=Relationships;Integrated Security=true;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Husband>()
//            .HasOne(h => h.Wife)
//            .WithOne(w => w.Husband)
//            .HasForeignKey<Wife>(w => w.WifeId);

//        modelBuilder.Entity<Wife>()
//            .HasKey(w => w.WifeId);
//    }
//}

#endregion

#region /*Default Convention*/

//public class Husband
//{
//    public int HusbandId { get; set; }

//    public string HusbandName { get; set; }

//    public string Age { get; set; }


//    public Wife Wife { get; set; } // navigation property
//}

//public class Wife
//{
//    public int WifeId { get; set; }

//    public string WifeName { get; set; }

//    public string Age { get; set; }

//    public int HusbandId { get; set; }
//    public Husband Husband { get; set; } // navigation property
//}

//public class MarriageContext : DbContext
//{
//    public DbSet<Husband> Husbands { get; set; }

//    public DbSet<Wife> Wives { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=GEDIKPC\\SQLEXPRESS;Database=Relationships;Integrated Security=true;");
//    }

//}

#endregion

#region /*Data Annotations - 1 */

//public class Husband
//{
//    public int HusbandId { get; set; }

//    public string HusbandName { get; set; }

//    public string Age { get; set; }


//    public Wife Wife { get; set; } // navigation property
//}

//public class Wife
//{
//    public int WifeId { get; set; }

//    public string WifeName { get; set; }

//    public string Age { get; set; }


//    [ForeignKey(nameof(Husband))]
//    public int HusbandId { get; set; }
//    public Husband Husband { get; set; } // navigation property
//}

//public class MarriageContext : DbContext
//{
//    public DbSet<Husband> Husbands { get; set; }

//    public DbSet<Wife> Wives { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=GEDIKPC\\SQLEXPRESS;Database=Relationships;Integrated Security=true;");
//    }

//}

#endregion

#region /*Data Annotations - 2 */

public class Husband
{
    public int HusbandId { get; set; }

    public string HusbandName { get; set; }

    public string Age { get; set; }


    public Wife Wife { get; set; } // navigation property
}

public class Wife
{
    [Key,ForeignKey(nameof(Husband))]
    public int WifeId { get; set; }

    public string WifeName { get; set; }

    public string Age { get; set; }



    public Husband Husband { get; set; } // navigation property
}


public class MarriageContext : DbContext
{
    public DbSet<Husband> Husbands { get; set; }

    public DbSet<Wife> Wives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=GEDIKPC\\SQLEXPRESS;Database=Relationships;Integrated Security=true;");
    }

}

#endregion