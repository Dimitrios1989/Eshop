using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.Products
{

    public class ProductViewModel 
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}