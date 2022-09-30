using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities

namespace Website_Ecommerce.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}