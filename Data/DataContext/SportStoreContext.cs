using Data.Entities;
using Data.Entities.OrderAggregate;
using Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContext
{
    public class SportStoreContext : IdentityDbContext<User,Role,Guid>
    {
        public SportStoreContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSKU> ProductSKU { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name="Customer",
                    NormalizedName="CUSTOMER"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
                );
            modelBuilder.Entity<Order>()
            .OwnsOne(o => o.ShippingAddress);
            modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Product_Name");
            modelBuilder.Entity<Product>()
           .HasIndex(p => p.SubCategoryId)
           .HasDatabaseName("IX_Product_SubCategoryId");

        }
    }
}
