using Eshop.ClassLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.ClassLibrary.Models.Products
{
    public class Product : ModelBase
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "The name can be maximum 50 characters long")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity cannot be less than zero.")]
        public int StockQuantity { get; set; }
        //public int? CategoryId { get; set; }
            
        ///// <summary>
        ///// Navigation Property
        ///// Εδώ εχουμε μια one-to-one σχεση. Ενα foreign key  με τις κατηγοριες προϊόντων
        ///// </summary>
        //public virtual ProductCategory Category { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual  ProductCategory Category { get; set; }
    }
}