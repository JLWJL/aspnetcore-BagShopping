    using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QualityBags.Data;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class ShoppingCart
    {
        
        public string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartID";

        public static ShoppingCart GetCart(HttpContext httpCntxt)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartID(httpCntxt);
            return cart;
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void AddToCart(Bag bag, ApplicationDbContext dbCntxt)
        {
            var cartItem = dbCntxt.CartItems.SingleOrDefault(i => i.CartID == ShoppingCartID && i.Bag.BagID ==bag.BagID );
            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartID = ShoppingCartID,
                    Bag = bag,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                dbCntxt.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            dbCntxt.SaveChanges();
            
        }

        /// <summary>
        /// Remove item from shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int RemoveFromCart(int id, ApplicationDbContext dbCntxt)
        {
            var cartItem = dbCntxt.CartItems.SingleOrDefault(c => c.Bag.BagID == id && c.CartID == ShoppingCartID);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    dbCntxt.CartItems.Remove(cartItem);
                }
                dbCntxt.SaveChanges();                
            }
            return itemCount;
        }

        /// <summary>
        /// Empty the shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
        public void EmtypCart(ApplicationDbContext dbCntxt)
        {
            var cartItems = dbCntxt.CartItems.Where(c => c.CartID == ShoppingCartID);
            //if(cartItems == null)
            //{
            //    //Do nothing
            //}
            foreach(var item in cartItems)
            {
                dbCntxt.CartItems.Remove(item);
            }

            dbCntxt.SaveChanges();
        }

        /// <summary>
        /// Get all items in a shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<CartItem> GetCartItems(ApplicationDbContext dbCntxt)
        {
            List<CartItem> cartItems = dbCntxt.CartItems.Include(i => i.Bag).Where(i => i.CartID == ShoppingCartID).ToList();
            return cartItems;
        }

        /// <summary>
        /// Get the total number of items in shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int GetCartItemsCount(ApplicationDbContext dbCntxt)
        {
            int? number = (from cartItems in dbCntxt.CartItems where cartItems.CartID == ShoppingCartID select (int?)cartItems.Count).Sum();
            return number ?? 0;
        }

        /// <summary>
        /// Get the total price of shopping cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public decimal GetCartTotal(ApplicationDbContext dbCntxt)
        {
            decimal? totalPrice = (from cartItems in dbCntxt.CartItems where cartItems.CartID == ShoppingCartID select (int?)cartItems.Count * cartItems.Bag.Price).Sum();
            return totalPrice ?? Decimal.Zero;
        }

        #region Helpers
        private string GetCartID(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid cartId = new Guid();
                context.Session.SetString(CartSessionKey, cartId.ToString());
            }

            return context.Session.GetString(CartSessionKey).ToString();

        }
        #endregion
    }
}
