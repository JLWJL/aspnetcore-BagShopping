using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QualityBags.Models;

namespace QualityBags.Data
{
    public class ShoppingContext:DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {

        }

        public DbSet<Bag> Bags { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bag>().ToTable("Bag");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
