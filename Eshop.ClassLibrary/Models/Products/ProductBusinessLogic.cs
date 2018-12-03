using Eshop.ClassLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.ClassLibrary.Models.Products
{
   public class ProductBusinessLogic
    {
        private MyContext db;
        public ProductBusinessLogic()
        {
            db = new MyContext();
        }

        public IQueryable<Product> GetProducts(ProductSearch searchModel)
        {
            var result = db.Products.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Name))
                    result = result.Where(x => x.Name.ToLower().Contains(searchModel.Name));
                if (!string.IsNullOrEmpty(searchModel.Description))
                    result = result.Where(x => x.Description.ToLower().Contains(searchModel.Description));
                if (searchModel.PriceFrom.HasValue)
                    result = result.Where(x => x.Price >= searchModel.PriceFrom);
                if (searchModel.PriceTo.HasValue)
                    result = result.Where(x => x.Price <= searchModel.PriceTo);
            }
            return result;
        }
    }
}
