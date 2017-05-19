using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualityBags.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QualityBags.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bag>().ToTable("Bag");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Supplier>().HasMany(s => s.Bags).WithOne("Supplier").OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Category>().HasMany(c => c.Bags).WithOne("Category").OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>().ToTable("CartItem");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<OrderItem>().HasOne(o => o.Order).WithMany(o => o.OrderItems).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
