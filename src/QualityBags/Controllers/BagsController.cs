using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBags.Data;
using QualityBags.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace QualityBags.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public BagsController(ApplicationDbContext context, IHostingEnvironment hstEnv)
        {
            _context = context;
            _hostingEnv = hstEnv;  
        }

        // GET: Bags
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {   
            var shoppingContext = _context.Bags.Include(b => b.Category).Include(b => b.Supplier);
            return View(await _context.Bags.ToListAsync());
        }

        // GET: Bags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.BagID == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // GET: Bags/Create
        public IActionResult Create()
        {
            PopulateCategoryList();
            PopulateSupplierList();
            return View();
        }

        // POST: Bags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BagID,BagName,CategoryID,Description,Price,SupplierID")] Bag bag, IFormFile uploadFile)
        {
            var relativeDir = "";
            var fileName = "";

            if (uploadFile == null)
            {
                relativeDir = "/images/Bags/Default.jpg";
            }
            else
            {
                fileName = ContentDispositionHeaderValue.Parse(uploadFile.ContentDisposition)
                                                                .FileName.Trim('"');
                relativeDir = "/images/Bags/"+ bag.BagName + "_" + fileName;
                using(FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeDir))
                {
                    await uploadFile.CopyToAsync(fs);
                    fs.Flush();
                }
            }
            bag.ImagePath = relativeDir;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(bag);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                PopulateCategoryList(bag.CategoryID);
                PopulateSupplierList(bag.SupplierID);
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("Image error", "Unable to uploade images for product. " +
                    "Try again, and if the problem persists " +
                    "contact your system administrator.");
            }
            
            return View(bag);
        }

        // GET: Bags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.BagID == id);
            if (bag == null)
            {
                return NotFound();
            }
            PopulateCategoryList(bag.CategoryID);
            PopulateSupplierList(bag.SupplierID);
            return View(bag);
        }

        // POST: Bags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, IFormFile uploadFile)
        {
            if (id == null)
            {
                return NotFound();
            }
            var editedBag = await _context.Bags.SingleOrDefaultAsync(b=>b.BagID==id);

            if (uploadFile != null)
            { 
                var fileName = ContentDispositionHeaderValue.Parse(uploadFile.ContentDisposition)
                                                                    .FileName.Trim('"');
                var relativeDir = "/images/Bags/" + editedBag.BagName + "_" + fileName;
                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeDir))
                    {
                        await uploadFile.CopyToAsync(fs);
                        fs.Flush();
                    }

                editedBag.ImagePath = relativeDir;
            }

            if (await TryUpdateModelAsync<Bag>(
                editedBag,
                "",
                b=>b.BagName, b=>b.Description, b => b.Price,
                b=>b.Description, b=>b.SupplierID, b => b.CategoryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error occurs when saving changes, try again or contact administrator");
                }
            }
            return View(editedBag);

            
        }

        // GET: Bags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.BagID == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // POST: Bags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.BagID == id);
            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BagExists(int id)
        {
            return _context.Bags.Any(e => e.BagID == id);
        }

        #region MyRegion
        private void PopulateCategoryList(object selectedCategory = null)
        {
            var categories = from cat in _context.Categories
                             orderby cat.CategoryID
                             select cat;
            ViewBag.CategoryNames = new SelectList(_context.Categories, "CategoryID", "CategoryName", selectedCategory);
        }

        private void PopulateSupplierList(object selectedSupplier = null)
        {
            var suppliers = from s in _context.Suppliers
                             orderby s.SupplierID
                             select s;
            ViewBag.SupplierNames = new SelectList(_context.Suppliers, "SupplierID", "FullName", selectedSupplier);
        }
        #endregion
    }

}
