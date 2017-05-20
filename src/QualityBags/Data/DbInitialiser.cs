using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBags.Models;
using Microsoft.EntityFrameworkCore;

namespace QualityBags.Data
{
    public class DbInitialiser
    {
        public static void Initialise(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated(); 
            context.Database.Migrate();  

            //if (context.Bags.Any())
            //{
            //    return;
            //}

            /*Pre-loading data*/
            //var suppliers = new Supplier[] {
            //    new Supplier{FirstName  = "Supplier1", LastName="1", PhoneMobile = "022121211", Email="supplier1@bags.com"},
            //    new Supplier{FirstName = "Supplier2", LastName ="2", PhoneMobile = "022121212", Email="supplier2@bags.com"},
            //    new Supplier{FirstName = "Supplier2", LastName ="3", PhoneMobile = "022121213", Email="supplier3@bags.com"},
            //};

            //foreach (var s in suppliers)
            //{
            //    context.Suppliers.Add(s);
            //}
            //context.SaveChanges();

            var categories = new Category[]
            {
                new Category {CategoryName="Wallets"},
                new Category {CategoryName="Purses"},
                new Category {CategoryName="Backpacks"}
            };
            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();


            //var bags = new Bag[]
            //{
            //    new Bag {BagName = "Bag1", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 1, SupplierID = 1},
            //    new Bag {BagName = "Bag2", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 1, SupplierID = 1},
            //    new Bag {BagName = "Bag3", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 2, SupplierID = 2},
            //    new Bag {BagName = "Bag4", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 2, SupplierID = 2},
            //    new Bag {BagName = "Bag5", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 3, SupplierID = 3},
            //};
            //foreach(var b in bags)
            //{
            //    context.Bags.Add(b);
            //}
            //context.SaveChanges();



        }
    }
}
