using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.ClassLibrary.Models.Products
{
    public class ProductSearch
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public bool IsEmpty
        {
            get
            {
                if (String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Description) && PriceFrom == null && PriceTo == null )
                    return true;
                else
                    return false;
            }

        }

    }
}

