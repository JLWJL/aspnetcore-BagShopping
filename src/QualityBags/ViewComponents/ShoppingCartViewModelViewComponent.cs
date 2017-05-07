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
            return View(GetShoppingCartViewModel());
        }

        public ShoppingCartViewModel GetShoppingCartViewModel()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(_context),
                CartTotal = cart.GetCartTotal(_context)
            };

            return viewModel;
        }
    }
}
