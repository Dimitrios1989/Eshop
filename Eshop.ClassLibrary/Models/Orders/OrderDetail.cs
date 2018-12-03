using Eshop.ClassLibrary.Models.Base;
using Eshop.ClassLibrary.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.Orders
{
    public class OrderDetail 
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
  
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal LineTotal { get; set; }
    }
}