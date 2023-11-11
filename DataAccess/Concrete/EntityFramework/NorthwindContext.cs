using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : database tabloları ile proje class'larını bağlamak için oluşturduğumuz class'tır.
    public class NorthwindContext : DbContext
    {
        // "OnConfiguring" metodu, projemizin hangi veritabanı ile ilişki olduğunu belirttiğimiz yerdir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }
        // Hangi classın(Product) hangi tabloya(Products) karşılık geldiğini tanımlıyoruz.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
