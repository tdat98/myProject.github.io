using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Inventory>().ToTable("Inventories");
            builder.Entity<Inventory>().HasKey(p => p.Id);
            builder.Entity<Inventory>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Inventory>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().HasMany(p => p.Stocks).WithOne(p => p.Inventory).HasForeignKey(p => p.InventoryId);

            builder.Entity<Stock>().ToTable("Stocks");
            builder.Entity<Stock>().HasKey(p => p.Id);
            builder.Entity<Stock>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Stock>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Stock>().Property(p => p.Noisx).IsRequired().HasMaxLength(30);
            builder.Entity<Stock>().Property(p => p.Soluong).IsRequired().HasMaxLength(30);


            builder.Entity<Unit>().ToTable("Units");
            builder.Entity<Unit>().HasKey(p => p.Id);
            builder.Entity<Unit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Unit>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            builder.Entity<Unit>().Property(p => p.Des).IsRequired().HasMaxLength(15);
            builder.Entity<Unit>().HasMany(p => p.Stocks).WithOne(p => p.Unit).HasForeignKey(p => p.UnitId);

            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Customer>().HasKey(p => p.Id);
            builder.Entity<Customer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            builder.Entity<Customer>().Property(p => p.Diachi).IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().Property(p => p.Sdt).IsRequired().HasMaxLength(30);
        }
    }
}
