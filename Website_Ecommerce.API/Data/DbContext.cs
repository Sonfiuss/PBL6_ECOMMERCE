using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne<Category>(p => p.Category)   //1 product - 1 category
                .WithMany(c => c.Products) //1 category - n product
                .HasForeignKey(p => p.CategoryId); // foreignKey of Product is CategoryId

            modelBuilder.Entity<ProductImage>()
                .HasOne<Product>(pi => pi.Product)  //1 image - 1 product
                .WithMany(p => p.ProductImages)   //1 product - n image
                .HasForeignKey(pi => pi.ProductId); //foreignKey of ProductImage is ProductId

            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId});
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Order>(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Product>(od => od.Product)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.ProductId);


            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId});
            modelBuilder.Entity<UserRole>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}