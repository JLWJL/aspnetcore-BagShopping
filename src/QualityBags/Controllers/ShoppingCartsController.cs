using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBags.Data;
using QualityBags.Models;

namespace QualityBags.Controllers
{
    public class ShoppingCartsController : Controller
    {
        //private readonly ApplicationDbContext _context;   /////  ??
        private readonly ApplicationDbContext _context;

        public ShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the shopping cart from Http context
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ShoppingCart currentCart = ShoppingCart.GetCart(this.HttpContext);
            return View(currentCart);
        }

        /// <summary>
        /// Add item to shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddToCart(int id, string returnurl = null)
        {
            //ViewBag.PreURL = Request.Headers["Referer"];
            var bagToAdd = _context.Bags.Single(b => b.BagID == id);
            if (bagToAdd != null)
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.AddToCart(bagToAdd, _context);
                //return RedirectToAction("Index", "CustomerBags");
                return Redirect(Request.Headers["Referer"].ToString()); //Stay at the same page after adding to cart
            }
            else
            {
                return NotFound();
            }
        }

        //public ActionResult AddToCart(int id)
        //{
        //    // Retrieve the album from the database
        //    var addedTutorial = _context.Bags
        //        .Single(b => b.BagID == id);
        //    // Add it to the shopping cart
        //    var cart = ShoppingCart.GetCart(this.HttpContext);
        //    cart.AddToCart(addedTutorial, _context);
        //    // Go back to the main store page for more shopping
        //    return RedirectToAction("Index", "ShoppingCarts");
        //}


        /// <summary>
        /// Remove or decrease the quantity of an item in shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id)
        {
            var bagToRemove = _context.Bags.Single(b => b.BagID == id);
            if (bagToRemove != null)
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                int itemCount = cart.RemoveFromCart(id, _context);
                return Redirect(Request.Headers["Referer"].ToString()); //Stay at the same page after adding to cart
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Empty the shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public IActionResult EmptyCart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmtypCart(_context);
            return Redirect(Request.Headers["Referer"].ToString()); //Stay at the same page after adding to cart
        }


        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.ShoppingCartID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingCartID")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.ShoppingCartID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ShoppingCartID")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.ShoppingCartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.ShoppingCartID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.ShoppingCartID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.ShoppingCartID == id);
            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ShoppingCartExists(string id)
        {
            return _context.ShoppingCart.Any(e => e.ShoppingCartID == id);
        }
    }
}
