﻿
using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Packt.Shared;
// позволяет управлять подключением к базе данных
public class Northwind : DbContext
{
    // эти свойства сопоставляются с таблицами в базе данных
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        //string connection = "Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Northwind; User ID = sa; Password = VjzLjxf2020!; Encrypt = False;MultipleActiveResultSets=True;";
        string connection = "Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Northwind; User ID = sa; Password = VjzLjxf2020!; Encrypt = False;MultipleActiveResultSets=True;";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating(
ModelBuilder modelBuilder)
    {
        // пример использования Fluent API вместо атрибутов,
        // чтобы ограничить длину имени категории 15 символами
        modelBuilder.Entity<Category>()
        .Property(category => category.CategoryName)
        .IsRequired() // NOT NULL
        .HasMaxLength(15);
        //if (ProjectConstants.DatabaseProvider == "SQLite")
        //{
        //    // добавлен патч для десятичной поддержки в SQLite
        //    modelBuilder.Entity<Product>()
        //    .Property(product => product.Cost)
        //    .HasConversion<double>();
        //}

        // глобальный фильтр для удаления снятых с производства товаров
        modelBuilder.Entity<Product>()
        .HasQueryFilter(p => !p.Discontinued);
    }
}
