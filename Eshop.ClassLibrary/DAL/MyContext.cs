using Eshop.ClassLibrary.Models.Base;
using Eshop.ClassLibrary.Models.Orders;
using Eshop.ClassLibrary.Models.Products;
using Eshop.ClassLibrary.Models.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.DAL
{
    public class MyContext:DbContext
    {
        public MyContext() : base("DefaultConnection")
        {
        }

        public static MyContext Create()
        {
            return new MyContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Product>().HasOptional(x => x.Category).WithMany();
        }

        public System.Data.Entity.DbSet<Eshop.ClassLibrary.Models.Roles.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<Eshop.ClassLibrary.Models.Roles.UserViewModel> UserViewModels { get; set; }
    }
}