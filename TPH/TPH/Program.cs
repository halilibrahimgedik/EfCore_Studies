// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");

ApplicationDbContext db = new();

#region Table Per Hierarcy Nedir ?
// Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.
#endregion

#region Neden TPH yaklaşımında bir tabloya ihtiyacımız olsun ?
// İçerisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları " Discriminator " kolonu üzerinden birbirlerinden ayırailiriz. İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama, veri ekleme, silme, vs. gibi operasyonların şekillendirilmesi için TPH davranışını kullanabiliriz.
#endregion

#region TPH Nasıl Uygulanır?
//EF Core'da entity aransında temel bir kalıtımsal ilişki söz konusuysa eğer default oalrak kabul edilen davranıştır.
//O yüzden herhangi bir konfigüreasyon gerektirmez!
//Entityler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir! 
#endregion

#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar.
//Discriminator kolonunu komple özelleştirebiliriz.
#endregion

#region Discriminator Değerleri Nasıl Değiştirilir ?
//Hiyerarşinin başındaki Entity konfigrasyonuna gelip, HasDiscriminator() fonksiyonu ile özelleştirmede bulunarak ardından HasValue() fonksiyonu ile hangi entitye karşılık hangi değerin girileceğini belirten türde ifade edebilirsiniz.
#endregion

#region TPH'da Veri Ekleme
//var employee1 = new Employee() {Name = "Halil", Surname = "Gedik",Department = "Yazılım"};

//var employee2 = new Employee() { Name = "Elif",Surname = "Gedik",Department = "Doktor"};

//var customer1 = new Customer() {Name = "Levent",Surname = "Karagöl",CompanyName = "Aphel A.Ş.", A=1};

//var customer2 = new Customer() {Name = "Ayşe",Surname = "Kaya",CompanyName = "Aphel A.Ş.", A = 1 };

//var technican1 = new Technician() { Name = "Fatma", Surname = "Kus", Department = "Bilişim", Branch = "Software Support" };
//db.Employees.AddRange(employee1, employee2);
//db.Customers.AddRange(customer1, customer2);
//db.Technicians.AddRange(technican1);
//db.SaveChanges();
#endregion NOT: verileri ekledikten sonra veri tabanını kontrol edersek Discriminator kolonunda "Employee,Customer,Technician gibi ifadeleri görürsünüz" Çünkü bu veriler discriminator sayesinde türünün hangi entity olduğu anlaşılmıştır ve oluşan tabloda diğer alanlar null olarak kalır.

#region TPH'da Veri Silme
//TPH davranışında silme operasyonu yine entity üzerinden gerçekleştirilir.

//var tech1 = db.Technicians.Find(7);
//if (tech1 != null)
//{
//    db.Technicians.Remove(tech1);
//    db.SaveChanges();
//}

#endregion

#region TPH'da Veri Güncelleme
//var query = db.Employees.Find(6);
//if (query != null)
//{
//    query.Name = "Alina";
//    db.Employees.Update(query);
//    db.SaveChanges();
//}
#endregion

#region TPH'da Veri Sorgulama
// Burada dikkat edilmesi gereken husus Technican entitysidir. Biz Employees leri sorgularsak , hepsini bir listeye çevirip ekrana yazdırırsak bize Technician ların da geldiğini göreceğiz, çünkü Technicianlarda bir Employee olduğundan dolayıdır.
//db.Technicians.Add(new Technician() { Name = "Sude", Surname = "Uyuz", Department = "Bilişim", Branch = "muhasebe" });
//db.SaveChanges();
//var employees = db.Employees.ToList();
//foreach (var employee in employees)
//{
//    Console.WriteLine(employee.Name+ " - " + employee.Department);
//}

// burada Sude Technician olmasına rağmen, Employeeleri listeye çevirip elkrana yazdırdığımızda Sude de gelmiştir.
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public int A { get; set; }
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
                    .HasDiscriminator<int>("EntityTürü")
                    .HasValue<Person>(1)
                    .HasValue<Employee>(2)
                    .HasValue<Customer>(3)
                    .HasValue<Technician>(4);

        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDB;Integrated Security=True");
    }
}