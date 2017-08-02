using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualityBags.Data;
using Microsoft.EntityFrameworkCore;
using QualityBags.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace QualityBags.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Display Home product page 
        */
        public async Task<IActionResult> Index()
        {
            int pagesize = 8;

            ViewBag.AllCategory = await _context.Categories.ToListAsync();//For category options

            //Pass sort selections
            ViewBag.Options = new List<String>
            {
                "Name",
                "Price"
            };

            //Retrieve all bags with navigation properties
            var Bags = (from bags in _context.Bags
                       select bags)
                       .Include(b => b.Category)
                       .Include(b => b.Supplier)
                       .OrderBy(b=>b.BagName);

            return View(await PageList<Bag>.CreateAsync(Bags.AsNoTracking(), 1, pagesize));
        }

        [HttpGet]
        public IActionResult FilteredBags(
            int? catID,
            int? curCat,
            string srcStr,
            string curFilter,
            string srtStr,
            int? page)
        {
            int pagesize = 8;

            var Bags = from bags in _context.Bags
                       select bags;
            Bags = Bags.Include(b => b.Category).Include(b => b.Supplier);

            //If users search, ignore category selection
            if (!String.IsNullOrEmpty(srcStr))
            {
                Bags = Bags.Where(b => b.BagName.Contains(srcStr)
                      || b.Category.CategoryName.Contains(srcStr));
            }

            if (catID != null)
            {
                Bags = GetBagsByCat(Bags, catID);
            }

            if (srtStr == "Price")
            {
                Bags = Bags.OrderByDescending(b => b.Price);
            }
            else
            {
                Bags = Bags.OrderBy(b => b.BagName);
            }

            var baglist = PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize);
            var bagsStr = JsonConvert.SerializeObject(baglist);

            return Json(bagsStr);
        }
        

        private IQueryable<Bag> GetBagsByCat(IQueryable<Bag> bags, int? catid)
        {
            IQueryable<Bag> Bags = bags.Where(b => b.Category.CategoryID == catid);
            return Bags;
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Who We Are";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "We'd Love To Hear Your Voice :)";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
