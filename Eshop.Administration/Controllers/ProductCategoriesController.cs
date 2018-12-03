using Eshop.ClassLibrary.DAL;
using Eshop.ClassLibrary.Models.Products;
using Eshop.ClassLibrary.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Administration.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private MyContext db = new MyContext();
        private IRepository<ProductCategory> _repo;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="pc"></param>
        public ProductCategoriesController(IRepository<ProductCategory> pc)
        {
            _repo = pc;
        }

        // GET: ProductCategories
        public ActionResult Index()
        {
            return View("Index", _repo.GetAll().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            try
            {
                var pc = db.ProductCategories.Where(x => x.Name == productCategory.Name).FirstOrDefault();
                if ( pc != null)
                {
                    return Content("Category already exists");
                }
                productCategory.DateCreated = DateTime.UtcNow;
                productCategory.CreatedBy = User.Identity.Name;

                _repo.Insert(productCategory);
                _repo.Save();

                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pc = _repo.GetById(id);
            if (pc == null)
            {
                return HttpNotFound();
            }
            return View(pc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductCategory productCategory)
        {
            try
            {
                if (productCategory != null)
                {
                    var pc = _repo.GetById(id);
                    if (pc != null)
                    {
                        pc.Name = productCategory.Name;
                        pc.DateUpdated = DateTime.UtcNow;
                        pc.UpdatedBy = User.Identity.Name;

                        _repo.Update(pc);
                        _repo.Save();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pc = _repo.GetById(id);
            if (pc == null)
            {
                return HttpNotFound();
            }
            return View(pc);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repo.Delete(id);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Content(ex.ToString());
            }
            
            //using (var db = new MyContext())
            //{
            //    ProductCategory pc = db.ProductCategories.Find(id);
            //    db.ProductCategories.Remove(pc);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
        }

    }
}