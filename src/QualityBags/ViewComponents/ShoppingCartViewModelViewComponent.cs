using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBags.Data;
using QualityBags.Models;
using QualityBags.Models.ShoppingCartViewModels;
using Microsoft.AspNetCore.Mvc;


namespace QualityBags.ViewComponents
{
    public class ShoppingCartViewModelViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartViewModelViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            //Returns a result which will render the partial view with name "Default"
            return View(GetShoppingCartViewModel());

            //Returns a result which will render the partial view with name viewName. 
            //return View("viewName", model);
        }

        public ShoppingCartViewModel GetShoppingCartViewModel()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(_context),
                CartTotal = cart.GetCartTotal(_context),
                GSTPrice = cart.GetGSTPrice(_context),
                CartSubTotal = cart.GetSubTotal(_context)
                
            };

            return viewModel;
        }
    }
}
