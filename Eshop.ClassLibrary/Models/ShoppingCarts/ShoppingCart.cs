using Eshop.ClassLibrary.DAL;
using Eshop.ClassLibrary.Models.Base;
using Eshop.ClassLibrary.Models.Orders;
using Eshop.ClassLibrary.Models.Products;
using Eshop.ClassLibrary.Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.ShoppingCarts
{
    public class ShoppingCart : ModelBase
    {
        private MyContext db = new MyContext();
        public const string CartSessionKey = "CartSessionKey";

        public string ShoppingCartId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();

            cart.ShoppingCartId = cart.GetCartId(context);

            return cart;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }

                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            else if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                context.Session[CartSessionKey] = context.User.Identity.Name;
            }

            return context.Session[CartSessionKey].ToString();
        }

        public List<ShoppingCart> GetCartItems()
        {
            return db.ShoppingCarts.Where(x => x.ShoppingCartId == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.ShoppingCarts
                              where cartItems.ShoppingCartId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public void AddToCart(Product product)
        {
            var cartItem = db.ShoppingCarts.SingleOrDefault(x => x.ShoppingCartId == ShoppingCartId && x.ProductId == product.Id);

            if (cartItem == null)
            {
                cartItem = new ShoppingCart
                {
                    ProductId = product.Id,
                    ShoppingCartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };
                db.ShoppingCarts.Add(cartItem);
                db.SaveChanges();
            }
            else
            {
                cartItem.Quantity += 1;
                db.SaveChanges();
            }
        }

        public void RemoveFromCart(int id)
        {
            var cartItem = db.ShoppingCarts.SingleOrDefault(cart => cart.ShoppingCartId == ShoppingCartId && cart.ProductId == id);

            if (cartItem != null)
            {
                db.ShoppingCarts.Remove(cartItem);
                db.SaveChanges();
            }
        }

        public void CreateOrderDetails(Order order)
        {
            decimal total = 0, lineTotal = 0;
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                lineTotal = (item.Quantity * item.Product.Price);
                total += lineTotal;

                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    LineTotal = lineTotal
                };
                
                db.OrderDetails.Add(orderDetail);
            }
            order.TotalAmount = total;
            db.SaveChanges();

            EmptyCart();
        }

        public void EmptyCart()
        {
            var cartItems = db.ShoppingCarts.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.ShoppingCarts.Remove(cartItem);
            }
            db.SaveChanges();
        }

    }
}