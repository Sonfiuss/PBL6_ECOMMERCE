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
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)   //1 product only 1 category
                .WithMany(c => c.Products) //1 category mutil product
                .HasForeignKey(p => p.CategoryId); // foreignKey of Product is CategoryId
 
            base.OnModelCreating(modelBuilder);
        }
    }
}