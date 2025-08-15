using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Packt.Shared;

public class Northwind : DbContext
{
    // эти свойства сопоставляются с таблицами в базе данных
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }

    public DbSet<Customer>? Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    
        //string connection = "Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Northwind; User ID = sa; Password = VjzLjxf2020!; Encrypt = False;MultipleActiveResultSets=True;";
        string connection = "Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Northwind; User ID = sa; Password = VjzLjxf2020!; Encrypt = False;MultipleActiveResultSets=True;";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating(
ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
    .Property(product => product.UnitPrice)
    .HasConversion<double>();
    }
}
