using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBags.Models;

namespace QualityBags.Data
{
    public class DbInitialiser
    {
        public static void Initialise(ShoppingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Bags.Any())
            {
                return;
            }

            /*Pre-loading data*/
            var suppliers = new Supplier[] {
                new Supplier{SupplierName = "Supplier1", Contact = 111111, Email="supplier1@bags.com"},
                new Supplier{SupplierName = "Supplier2", Contact = 222222, Email="supplier2@bags.com"},
                new Supplier{SupplierName = "Supplier3", Contact = 333333, Email="supplier3@bags.com"},
            };
            foreach (var s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var categories = new Category[]
            {
                new Category {CategoryName="Men"},
                new Category {CategoryName="Women"},
                new Category {CategoryName="Children"}
            };
            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();


            var bags = new Bag[]
            {
                new Bag {BagName = "Bag1", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 1, SupplierID = 1},
                new Bag {BagName = "Bag2", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 1, SupplierID = 1},
                new Bag {BagName = "Bag3", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 2, SupplierID = 2},
                new Bag {BagName = "Bag4", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 2, SupplierID = 2},
                new Bag {BagName = "Bag5", Price = 20.00, Description ="Good bag1", ImagePath="", CategoryID = 3, SupplierID = 3},
            };
            foreach(var b in bags)
            {
                context.Bags.Add(b);
            }
            context.SaveChanges();



        }
    }
}
