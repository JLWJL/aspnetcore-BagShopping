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


        public async Task<IActionResult> Index()
        {
            int pagesize = 8;

            //UpdateCatForAllBags();
            ViewBag.AllCategory = await _context.Categories.ToListAsync();//For category options

            //Pass sort selections 1
            ViewBag.SortSelection = new List<SelectListItem> {
                    new SelectListItem {Text="Name", Value= "name" },
                    new SelectListItem { Text = "Price", Value = "price" }
            };

            //Pass sort selections 2
            ViewBag.Options = new List<String>
            {
                "Name",
                "Price"
            };

            //ViewData["CurSort"] = srtStr;//Store current sort for pagination in the view

            ////When users search, go back to page 1
            ////Otherwise keep the current search condition to retrieve data
            //if (srcStr != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    srcStr = curFilter;
            //}
            //ViewData["CurSearch"] = srcStr; //Store current search for pagination in the view

            ////When users choose category, go back to page 1
            ////Otherwise keep current category selection to retrieve data
            //if (catID != null)  //When no new catID passed in, catID here is the last one
            //{
            //    page = 1;
            //}
            //else
            //{
            //    catID = curCat;
            //}
            //ViewData["CurCat"] = catID;  //Store the current category for pagination in the view

            //Retrieve all bags with navigation properties
            //Then filter data according to parameters
            var Bags = from bags in _context.Bags
                       select bags;
            Bags = Bags.Include(b => b.Category).Include(b => b.Supplier);

            ////If users search, ignore category selection
            //if (!String.IsNullOrEmpty(srcStr))
            //{
            //    Bags = Bags.Where(b => b.BagName.Contains(srcStr) || b.Category.CategoryName.Contains(srcStr));
            //}

            //if (catID != null)
            //{
            //    Bags = GetBagsByCat(Bags, catID);
            //}

            //if (srtStr == "price")
            //{
            //    Bags = Bags.OrderByDescending(b => b.Price);
            //}
            //else
            //{
            //    Bags = Bags.OrderBy(b => b.BagName);
            //}

            /*View by category*/

            //var applicationDbContext = _context.Bags.Include(b => b.Category).Include(b => b.Supplier);
            return View(await PageList<Bag>.CreateAsync(Bags.AsNoTracking(), 1, pagesize));
        }

        [HttpGet]
        public async Task<IActionResult> FilteredBags(
            int? catID,
            int? curCat,
            string srcStr,
            string curFilter,
            string srtStr,
            int? page)
        {
            int pagesize = 8;

            ViewData["CurSort"] = srtStr;//Store current sort for pagination in the view

            //When users search, go back to page 1
            //Otherwise keep the current search condition to retrieve data
            if (srcStr != null)
            {
                page = 1;
            }
            else
            {
                srcStr = curFilter;
            }
            ViewData["CurSearch"] = srcStr; //Store current search for pagination in the view

            //When users choose category, go back to page 1
            //Otherwise keep current category selection to retrieve data
            if (catID != null)  //When no new catID passed in, catID here is the last one
            {
                page = 1;
            }
            else
            {
                catID = curCat;
            }
            ViewData["CurCat"] = catID;  //Store the current category for pagination in the view

            var Bags = from bags in _context.Bags
                       select bags;
            //Bags = Bags.Include(b => b.Category).Include(b => b.Supplier);

            //If users search, ignore category selection
            if (!String.IsNullOrEmpty(srcStr))
            {
                Bags = Bags.Where(b => b.BagName.Contains(srcStr) || b.Category.CategoryName.Contains(srcStr));
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

            //var json= JsonConvert.SerializeObject(PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize));
            //var json  = Json(PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize));
            //return json;

            var baglist = PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize);
            var jbags = baglist.Result.ToString();

            return Json(JsonConvert.SerializeObject(baglist));

            //var jsonbags = PageList<Bag>.CreateAsync(Bags.AsNoTracking(), page ?? 1, pagesize).Result;
            //return jsonbags;
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
