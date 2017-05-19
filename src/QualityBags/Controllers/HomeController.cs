using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualityBags.Data;
using Microsoft.EntityFrameworkCore;
using QualityBags.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QualityBags.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? catID, int? currentCat, int? page, string searchStr = null, string sortStr = null)
        {
            int pagesize = 8;
            //UpdateCatForAllBags();
            ViewBag.AllCategory = await _context.Categories.ToListAsync();/*Pass all categories into view*/
            ViewBag.SortSelection = new SelectList(new List<object> {}); //Pass sort selections

            //Store search condition and sort condition
            ViewData["SearchString"] = searchStr;
            ViewData["SortString"] = sortStr;

            /*View by category*/
            var Bags = from bags in _context.Bags //Retrieve all bags 
                       select bags;  
            Bags = Bags.Include(b => b.Category).Include(b=>b.Supplier);

            if (catID!=null)   //If filter exists, retrieve those bags
            {
                page = 1;
            }
            else
            {
                catID = currentCat;
            }

            ViewData["CurrentCat"] = catID;  //Store the current selection of category

            if (catID != null)  //When no new catID passed in, catID here is the last one
            {
                Bags = GetBagsByCat(Bags, catID);
            }

            //var applicationDbContext = _context.Bags.Include(b => b.Category).Include(b => b.Supplier);
            return View(await PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize));
        }

        private IQueryable<Bag> GetBagsByCat(IQueryable<Bag> bags, int? catid)
        {
            IQueryable<Bag> Bags = bags.Where(b => b.Category.CategoryID==catid);
            return Bags;
        }

        //private void UpdateCatForAllBags()
        //{
        //    var bags = from b in _context.Bags
        //               select b;
        //    foreach(var b in bags)
        //    {
        //        b.Category = _context.Categories.Single(c => c.CategoryID == b.CategoryID);
        //        b.Supplier = _context.Suppliers.Single(s => s.SupplierID == b.SupplierID);
        //        _context.Update(b);
        //    }
        //    _context.SaveChanges();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
