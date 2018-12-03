using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.ShoppingCarts
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }

}